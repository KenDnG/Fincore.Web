using FINCORE.Models.Models.Acquisition;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Services.Acquisition;
using Microsoft.AspNetCore.Mvc;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class CAController : Controller
    {
        private CAServices services = new CAServices();

        public IActionResult Index()
        {
            return View("~/Views/Acquisition/CA/Index.cshtml");
            //return View();
        }

        public IActionResult List(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<CAModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPagingList(string.Empty, page, Paginations.MaxPerPage);
                dataPaging = (PaginationModels<CAModels>)data.Result.data;
            }
            else
            {
                page = 1;
                var data = services.GetPagingList(searchTerm, page, Paginations.MaxPerPage);
                dataPaging = (PaginationModels<CAModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            ViewBag.PagingCA = dataPaging;
            ViewBag.CAModels = dataPaging.Model;
            return View("~/Views/Acquisition/CA/List.cshtml");
            //return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var data = services.GetDataCA().Result.data;
            return Ok(data);
        }
    }
}