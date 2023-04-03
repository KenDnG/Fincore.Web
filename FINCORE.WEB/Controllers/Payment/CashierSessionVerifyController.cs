using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Payment.CashierSessionVerify;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Payment;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using System.ComponentModel.DataAnnotations;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Payment
{
    //[BindProperties]
    public class CashierSessionVerifyController : Controller
    {
        private readonly CashierSessionVerifyServices cashierSessionVerifyService = new();

        #region Get Session
        private T GetSession<T>(string sesseionKey)
        {
            return HttpContext.Session.GetSessionFromJson<T>(sesseionKey);
        }
        #endregion

        #region Pagination
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                string branchId = GetSession<string>("branch_id");
                int page = 1;

                APIResponse data = cashierSessionVerifyService.GetPaginationCashierSessionVerify(branchId, page, Paginations.MaxPerPageLookup, String.Empty);
                PaginationModels<PaginationCashierSessionVerifyModels> dataPaging = (PaginationModels<PaginationCashierSessionVerifyModels>)data.data;

                ViewBag.CurrentPage = page;

                return View("~/Views/Payment/CashierSessionVerify/Index.cshtml", dataPaging);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public IActionResult Index(RequestParam requestParam)
        {
            try
            {
                string branchId = GetSession<string>("branch_id");
                int page = 1;

                APIResponse data = cashierSessionVerifyService.GetPaginationCashierSessionVerify(branchId, requestParam.PageIndex, Paginations.MaxPerPageLookup, requestParam.SearchTerm);
                PaginationModels<PaginationCashierSessionVerifyModels> dataPaging = (PaginationModels<PaginationCashierSessionVerifyModels>)data.data;
                dataPaging.SearchTerm = requestParam.SearchTerm ?? "";

                ViewBag.CurrentPage = page;

                return View("~/Views/Payment/CashierSessionVerify/Index.cshtml", dataPaging);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        #endregion

        #region View Detail TransactionDetailReceipt
        [HttpPost]
        public IActionResult TransactionDetailReceipt(RequestParamReceipt requestParamReceipt)
        {
            try
            {
                int page = 1;
                if (requestParamReceipt.PageIndex == 0)
                {
                    requestParamReceipt.PageIndex = 1;
                }

                requestParamReceipt = new RequestParamReceipt
                {
                    PageIndex = requestParamReceipt.PageIndex,
                    MaxPage = Paginations.MaxPerPageLookup,
                    BranchId = GetSession<string>("branch_id"),
                    SessionId = requestParamReceipt.SessionId,
                    SearchTerm = requestParamReceipt.SearchTerm
                };

                APIResponse dataReceipt = cashierSessionVerifyService.GetCashierSessionDetailReceipt(requestParamReceipt);
                PaginationModels<TransactionDetailReceiptModels> dataReceiptPaging = (PaginationModels<TransactionDetailReceiptModels>)dataReceipt.data;
                dataReceiptPaging.SearchTerm = requestParamReceipt.SearchTerm ?? "";

#pragma warning disable CS8604 // Possible null reference argument.
                if (dataReceipt.code == StatusCodes.Status500InternalServerError)
                {
                    TempData["Error"] = dataReceipt.message;

                    return RedirectToAction("Index");
                }
                else if (!dataReceiptPaging.Model.Any())
                {
                    dataReceiptPaging = new PaginationModels<TransactionDetailReceiptModels>
                    {
                        PageIndex = 0,
                        PageSize = 5,
                        RecordCount = 0,
                        SearchTerm = "",
                        TotalPages = 0,
                        Model = new List<TransactionDetailReceiptModels>
                            {
                                new TransactionDetailReceiptModels
                                {
                                    SessionId = requestParamReceipt.SessionId,
                                }
                            }
                    };
                }
#pragma warning restore CS8604 // Possible null reference argument.

                ViewBag.CurrentPage = page;
                return View("~/Views/Payment/CashierSessionVerify/TransactionDetailReceipt.cshtml", dataReceiptPaging);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region View Detail TransactionDetailExpenditure
        [HttpPost]
        public IActionResult TransactionDetailExpenditure(RequestParamExpenses requestParamExpenses)
        {
            try
            {
                int page = 1;
                if (requestParamExpenses.PageIndex == 0)
                {
                    requestParamExpenses.PageIndex = 1;
                }

                requestParamExpenses = new RequestParamExpenses
                {
                    PageIndex = requestParamExpenses.PageIndex,
                    MaxPage = Paginations.MaxPerPageLookup,
                    BranchId = GetSession<string>("branch_id"),
                    SessionId = requestParamExpenses.SessionId,
                    SearchTerm = requestParamExpenses.SearchTerm
                };

                APIResponse dataExpenses = cashierSessionVerifyService.GetCashierSessionDetailExpenses(requestParamExpenses);
                PaginationModels<TransactionDetailExpensesModels> dataExpensesPaging = (PaginationModels<TransactionDetailExpensesModels>)dataExpenses.data;
                dataExpensesPaging.SearchTerm = requestParamExpenses.SearchTerm ?? "";

#pragma warning disable CS8604 // Possible null reference argument.
                if (dataExpenses.code == StatusCodes.Status500InternalServerError)
                {
                    TempData["Error"] = dataExpenses.message;

                    return RedirectToAction("Index");
                }
                else if (!dataExpensesPaging.Model.Any())
                {
                    dataExpensesPaging = new PaginationModels<TransactionDetailExpensesModels>
                    {
                        PageIndex = 0,
                        PageSize = 5,
                        RecordCount = 0,
                        SearchTerm = "",
                        TotalPages = 0,
                        Model = new List<TransactionDetailExpensesModels>
                            {
                                new TransactionDetailExpensesModels
                                {
                                    SessionId = requestParamExpenses.SessionId,
                                }
                            }
                    };
                }
#pragma warning restore CS8604 // Possible null reference argument.

                ViewBag.CurrentPage = page;
                return View("~/Views/Payment/CashierSessionVerify/TransactionDetailExpenditure.cshtml", dataExpensesPaging);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Verify
        [HttpPost]
        public IActionResult Verify(string SessionId)
        {
            try
            {
                LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
                var employeeId = lDAPModels.EmployeeId;
                string branchId = GetSession<string>("branch_id");

                APIResponse data = cashierSessionVerifyService.Verify(SessionId, employeeId, branchId);

                switch (data.code)
                {
                    case StatusCodes.Status500InternalServerError:
                        TempData["Error"] = data.message;
                        break;
                    case StatusCodes.Status400BadRequest:
                        TempData["Error"] = data.message;
                        break;
                    case StatusCodes.Status403Forbidden:
                        TempData["Warning"] = data.message;
                        break;
                    default:
                        TempData["Success"] = "Session verified successfully";
                        break;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
