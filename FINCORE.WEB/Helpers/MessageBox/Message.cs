using FINCORE.Services.Helpers.Response;
using Microsoft.AspNetCore.Mvc;

namespace FINCORE.WEB.Helpers.MessageBox
{
    //Note: Need & Continuous improvement

    /// <summary>
    /// Call Message Alert with this Class (method: AlertError, AlertInfo, AlertSuccess, AlertWarning)
    /// </summary>
    public class Message : Controller, IMessage
    {
        public IActionResult AlertError(string message, string viewName)
        {
            ViewBag.State = Collection.Status.FAILED;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = false;

            return View(viewName);
        }

        public IActionResult AlertInfo(string message, string viewName)
        {
            ViewBag.State = Collection.Status.INFO;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = false;

            return View(viewName);
        }

        public IActionResult AlertInfoThenDirect(string message, IActionResult directTo)
        {
            ViewBag.State = Collection.Status.INFO;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = true;

            return directTo;
        }

        public IActionResult AlertSuccess(string message, string viewName)
        {
            ViewBag.State = Collection.Status.SUCCESS;
            ViewBag.Message = message;

            return View(viewName);
        }

        public IActionResult AlertSuccessThenDirect(string message, IActionResult directTo)
        {
            ViewBag.State = Collection.Status.SUCCESS;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = true;

            return directTo;
        }

        public IActionResult AlertWarning(string message, string viewName)
        {
            ViewBag.State = Collection.Status.WARNING;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = false;

            return View(viewName);
        }

        public IActionResult AlertWarningThenDirect(string message, IActionResult directTo)
        {
            ViewBag.State = Collection.Status.WARNING;
            ViewBag.Message = message;
            ViewBag.ConfirmDialog = true;

            return directTo;
        }
    }
}