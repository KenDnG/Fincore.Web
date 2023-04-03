using FINCORE.Models.Models.Acquisition;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.CRUD;
using FINCORE.Services.Services.LDAP;
using Microsoft.AspNetCore.Mvc;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers
{
    public class CRUDController : Controller
    {
        private CRUDServices services = new CRUDServices();

        public IActionResult Index()
        {
            LDAPServices ldap = new LDAPServices();
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult List(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<XCASModel>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPagingList(string.Empty, page, Paginations.MaxPerPage);
                dataPaging = (PaginationModels<XCASModel>)data.Result.data;
            }
            else
            {
                page = 1;
                var data = services.GetPagingList(searchTerm, page, Paginations.MaxPerPage);
                dataPaging = (PaginationModels<XCASModel>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            ViewBag.PagingCAS = dataPaging;
            ViewBag.CASModel = dataPaging.Model;
            return View();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] CASModels items)
        {
            var data = services.InsertCRUD(items);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult InsertRazor([Bind] CASModels items)
        {
            var data = services.InsertCRUD(items);
            if (data.Result.status == Collection.Status.SUCCESS)
            {
                ViewBag.State = Collection.Status.SUCCESS;
            }
            else
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = data.Result.message;
            }
            return View("Add");
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var data = services.GetDataCRUD().Result.data;
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Update([FromBody] CASModels items)
        {
            var data = services.UpdateCRUD(items);
            return Ok(data);
        }
    }
}