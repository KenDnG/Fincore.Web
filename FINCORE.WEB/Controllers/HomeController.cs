using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Masters;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static FINCORE.Services.Helpers.Response.Collection;

namespace FINCORE.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MenuTreeServices menutree = new MenuTreeServices();
        private MsPromotionLineTextServices promLineTextServices = new MsPromotionLineTextServices();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string branch_id, string branch_name)
        {
            var s = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            if (s == null)
            {
                return RedirectToAction("Index", "Login");
            }

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            APIResponse result = new APIResponse();
            string controllerName = "", actionName = "";
            var res = menutree.GetMenuTree(lDAPModels.EmployeeId).Result;
            var promotionText = await promLineTextServices.GetPromotionLineTextList();

            if (res.code == Codes.SUCCESS)
            {
                List<MenuTree> MenuTreeList = JsonConvert.DeserializeObject<List<MenuTree>>(res.data.ToString());
                result = new APIResponse()
                {
                    code = Collection.Codes.SUCCESS
                };
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.USER_MENU_ACCESS, MenuTreeList);
            }

            if (promotionText.code == Codes.SUCCESS)
            {
                var promTextList = promotionText.data;
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.PROMOTION_LINE_TEXT, promTextList);
            }

            if (!String.IsNullOrEmpty(branch_id))
            {
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.BRANCH_ID, branch_id);
            }
            if (!String.IsNullOrEmpty(branch_name))
            {
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.BRANCH_NAME, branch_name);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}