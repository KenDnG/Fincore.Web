using FINCORE.Models.Models.Cashier;
using FINCORE.Models.Models.LDAP;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Session;
using FINCORE.WEB.Helpers;
using FINCORE.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Session
{
    //[Authorize]
    [BindProperties]
    public class CSSessionController : Controller
    {
        private readonly CashierSessionServices _services = new();

        [HttpGet]
        public IActionResult Index()
        {
            string branchId = GetSession<string>(SessionKey.Branchid);

            PaginationModels<PaginationCashierSessionModels> dataPaging = new();

            int page = 1;

            APIResponse data = _services.GetPaginationCashierSession(branchId, page, Paginations.MaxPerPageLookup, String.Empty);

            dataPaging = (PaginationModels<PaginationCashierSessionModels>)data.data;

            ViewBag.CurrentPage = page;

            return View(dataPaging);
        }

        [HttpPost]
        public IActionResult Index(RequestParams requestParams)
        {
            string branchId = GetSession<string>(SessionKey.Branchid);

            PaginationModels<PaginationCashierSessionModels> dataPaging = new();

            int page = 1;

            APIResponse data = _services.GetPaginationCashierSession(branchId,
                requestParams.pageIndex, Paginations.MaxPerPageLookup, requestParams.searchTerm);

            dataPaging = (PaginationModels<PaginationCashierSessionModels>)data.data;
            dataPaging.SearchTerm = requestParams.searchTerm ?? "";

            ViewBag.CurrentPage = page;

            return View(dataPaging);
        }

        [HttpPost]
        public IActionResult ViewDetail(string sessionId)
        {
            var model = new PaginationModels<PaginationCashierSessionDetailModels>
            {
                PageIndex = 1,
                PageSize = 5,
                RecordCount = 10,
                SearchTerm = "",
                TotalPages = 2,
                Model = new List<PaginationCashierSessionDetailModels>
                            {
                                new PaginationCashierSessionDetailModels
                                {
                                    SessionId = sessionId,
                                    BranchId = "00100",
                                    DateOfRecipt = DateTime.Now,
                                    IncomeSource = "Cash",
                                    PayAmount = 500000,
                                    PayDate = DateTime.Now,
                                    ReferenceNumber = "1234567890"
                                },
                                new PaginationCashierSessionDetailModels
                                {
                                    SessionId = sessionId,
                                    BranchId = "00100",
                                    DateOfRecipt = DateTime.Now,
                                    IncomeSource = "Cash",
                                    PayAmount = 500000,
                                    PayDate = DateTime.Now,
                                    ReferenceNumber = "1234567890"
                                },
                                new PaginationCashierSessionDetailModels
                                {
                                    SessionId = sessionId,
                                    BranchId = "00100",
                                    DateOfRecipt = DateTime.Now,
                                    IncomeSource = "Cash",
                                    PayAmount = 500000,
                                    PayDate = DateTime.Now,
                                    ReferenceNumber = "1234567890"
                                },
                                new PaginationCashierSessionDetailModels
                                {
                                    SessionId = sessionId,
                                    BranchId = "00100",
                                    DateOfRecipt = DateTime.Now,
                                    IncomeSource = "Cash",
                                    PayAmount = 500000,
                                    PayDate = DateTime.Now,
                                    ReferenceNumber = "1234567890"
                                },
                                new PaginationCashierSessionDetailModels
                                {
                                    SessionId = sessionId,
                                    BranchId = "00100",
                                    DateOfRecipt = DateTime.Now,
                                    IncomeSource = "Cash",
                                    PayAmount = 500000,
                                    PayDate = DateTime.Now,
                                    ReferenceNumber = "1234567890"
                                }
                            }
            };

            return View(model);
        }

        public IActionResult AddSession()
        {
            string branchId = GetSession<string>(SessionKey.Branchid);
            LDAPModels? user = GetSession<LDAPModels>(SessionKey.User);

            APIResponse data = _services.InsertNewCashierSession(user.EmployeeId, user.GivenName, branchId);

            if (data.code == StatusCodes.Status403Forbidden)
            {
                TempData["Warning"] = "Tidak bisa membuat session baru!, masih ada session yang belum di close";
            }
            else
            {
                TempData["Success"] = $"Session baru berhasil dibuat";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CloseSession(CloseSessionDTO closeSessionDTO)
        {
            if (!ModelState.IsValid)
            {
                TempData["Warning"] = "Input Invalid";
                return RedirectToAction("Index");
            }

            LDAPModels? user = GetSession<LDAPModels>("SessionUser");

            APIResponse data = _services
                .CloseCashierSession(closeSessionDTO.SessionId, user.GivenName, closeSessionDTO.CurrentAmount, closeSessionDTO.CashAmount);

            switch (data.code)
            {
                case StatusCodes.Status500InternalServerError:
                    TempData["Error"] = "Session gagal di CLose, coba lagi nanti";
                    break;

                case StatusCodes.Status403Forbidden:
                    TempData["Error"] = "Session telah ditutup oleh admin lain";
                    break;

                default:
                    TempData["Success"] = "Session berhasil di close";
                    break;
            }

            return RedirectToAction("Index");
        }

        public IActionResult PrintKwitansi()
        {
            throw new NotImplementedException();
        }

        public IActionResult PrintDTL()
        {
            throw new NotImplementedException();
        }

        private T GetSession<T>(string sessionKey)
        {
            return Sessions.GetSessionFromJson<T>(HttpContext.Session, sessionKey);
        }
    }

    public class CloseSessionDTO
    {
        [Required]
        [StringLength(13)]
        public string? SessionId { get; set; }

        [Required]
        public double CurrentAmount { get; set; }

        [Required]
        public double CashAmount { get; set; }
    }
}