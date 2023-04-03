using Microsoft.AspNetCore.Mvc;

namespace FINCORE.WEB.Controllers.Termination
{
    public class ReposessionController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Termination/Reposession/Index.cshtml");
        }
    }
}