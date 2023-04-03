using FINCORE.Models.Models.Acquisition.OMA;
using FINCORE.Models.Models.Acquisition.OMA.Paging;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Models.ViewModels;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;
using Route = FINCORE.Services.Helpers.Domain.EndpointAPI.Route;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class OMAController : Controller
    {
        private OMAServices services = new OMAServices();

        public IActionResult Index(string id, string status)
        {
            try
            {
                OMAModel OMAdata = (OMAModel)services.GetOMAById(id).Result.data;
                ViewModelOMA VM = new ViewModelOMA();
                VM.OMAModel = OMAdata;
                ViewBag.View = "False";
                if (status == "View")
                {
                    ViewBag.View = "True";
                }
                return View("~/Views/Acquisition/OMA/Index.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        public IActionResult List(int pageIndex, string searchTerm)
        {
            try
            {
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");
                var dataPaging = new PaginationModels<OMAPaging>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetOMAPaging(string.Empty, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<OMAPaging>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetOMAPaging(searchTerm, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<OMAPaging>)data.Result.data;
                }
                ViewBag.CurrentPage = page;
                ViewBag.PagingData = dataPaging;
                ViewBag.OMAModels = dataPaging.Model;
                ViewBag.Message = Sessions.GetSessionFromJson<string>(HttpContext.Session, "msg");

                ViewBag.State = Sessions.GetSessionFromJson<string>(HttpContext.Session, "state");

                Sessions.RemoveSessionByKey(HttpContext.Session, "msg");
                Sessions.RemoveSessionByKey(HttpContext.Session, "state");
                return View("~/Views/Acquisition/OMA/List.cshtml");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Update([Bind] ViewModelOMA item, string Status)
        {
            string destinationpath = @$"{Route.OMA_UPLOAD_PATH}{DateTime.Now.Year.ToString()}\{DateTime.Now.Month.ToString()}\{DateTime.Now.Day.ToString()}\";//change destination path here
            var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            string state;
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (item.slikpemohon != null)
                    {
                        item.OMAModel.SLIK_file_name_pemohon = item.slikpemohon.FileName;
                        item.OMAModel.SLIK_file_path_pemohon = destinationpath;
                        CopyFile(item.slikpemohon, destinationpath);
                    }
                    if (item.slikpasangan != null)
                    {
                        item.OMAModel.SLIK_file_name_pasangan = item.slikpasangan.FileName;
                        item.OMAModel.SLIK_file_path_pasangan = destinationpath;
                        CopyFile(item.slikpasangan, destinationpath);
                    }
                    if (item.appifile != null)
                    {
                        item.OMAModel.APPI_file_name = item.appifile.FileName;
                        item.OMAModel.APPI_file_path = destinationpath;
                        CopyFile(item.appifile, destinationpath);
                    }

                    //change status and send model to service
                    item.OMAModel.status_order = Status;
                    var data = services.UpdateOMA(item.OMAModel);
                    if (data.Result.status == Collection.Status.SUCCESS)
                    {
                        state = Collection.Status.SUCCESS;
                    }
                    else
                    {
                        state = Collection.Status.FAILED;
                        msg = data.Result.message;
                    }
                    Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                    Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                }
                else
                {
                    ViewBag.View = "False";
                    return View("~/Views/Acquisition/OMA/Index.cshtml", item);
                }
            }
            catch (Exception ex)
            {
                state = Collection.Status.FAILED;
                msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);

                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }

        public IActionResult GetFoto(string Path)
        {
            byte[] file = System.IO.File.ReadAllBytes(Path);
            string file_type = "image/jpg";
            return File(file, file_type);
        }

        [HttpGet]
        public IActionResult GetPDF(string Path, string FileName)
        {
            var data = Path + FileName;
            byte[] file = System.IO.File.ReadAllBytes(data);
            Response.Headers.Add("Content-Disposition", "attachment;filename=" + FileName);
            return File(file, GetContentType(data));
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("List");
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private void CopyFile(IFormFile uploadedfile, string destinationpath)
        {
            byte[] pdfbytes = null;
            var tempfilepath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(tempfilepath))
            {
                uploadedfile.CopyTo(stream);
            }
            pdfbytes = System.IO.File.ReadAllBytes(tempfilepath);
            DirectoryInfo di = Directory.CreateDirectory(destinationpath);
            var path = Path.Combine(destinationpath, Path.GetFileName(uploadedfile.FileName));
            System.IO.File.WriteAllBytes(path, pdfbytes);
        }
    }
}