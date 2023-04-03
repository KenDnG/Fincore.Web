using FINCORE.Models.Models.Approval;
using FINCORE.Models.Models.LDAP;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Approval;
using FINCORE.WEB.Helpers;
using FINCORE.WEB.Models;
using Microsoft.AspNetCore.Mvc;

//using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Approval
{
    //[Authorize]
    public class ApprovalController : Controller
    {
        private readonly ApprovalServices services = new();

        //public IActionResult Inbox([FromQuery] RequestParams requestParams)
        [HttpGet]
        public IActionResult Inbox()
        {
            LDAPModels? user = GetUserData();

            PaginationModels<PaginationInboxApprovalModels> dataPaging = new();
            int page = 1; // requestParams.pageIndex == 0 ? 1 : requestParams.pageIndex;

            var employeeId = "0"; // hardcode for sample user object

            if (user is not null && !string.IsNullOrEmpty(user.EmployeeId))
            {
                employeeId = user.EmployeeId;
            }

            //hard code temp
            //employeeId = "220080573";

            APIResponse data = services.GetPaginationInboxApproval(employeeId, page, Paginations.MaxPerPageLookup, String.Empty);
            dataPaging = (PaginationModels<PaginationInboxApprovalModels>)data.data;

            ViewBag.CurrentPage = page;

            return View("~/Views/Approval/Inbox.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult Inbox(RequestParams requestParams)
        {
            LDAPModels? user = GetUserData();

            PaginationModels<PaginationInboxApprovalModels> dataPaging = new();
            int page = requestParams.pageIndex == 0 ? 1 : requestParams.pageIndex;

            //var employeeId = "220080573"; // hardcode for sample user object

            var employeeId = "0"; // hardcode for sample user object

            if (user is not null && !string.IsNullOrEmpty(user.EmployeeId))
            {
                employeeId = user.EmployeeId;
            }

            //hard code temp
            //employeeId = "220080573";

            APIResponse data = services.GetPaginationInboxApproval(employeeId, page, Paginations.MaxPerPageLookup, requestParams.searchTerm);
            dataPaging = (PaginationModels<PaginationInboxApprovalModels>)data.data;

            ViewBag.CurrentPage = page;

            return View("~/Views/Approval/Inbox.cshtml", dataPaging);
        }

        private LDAPModels GetUserData() => Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");
    }
}