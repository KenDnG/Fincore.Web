using FINCORE.Models.Models.LDAP;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static FINCORE.Services.Helpers.Response.Collection;

namespace FINCORE.WEB.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionUser = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            if (sessionUser == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "Index"
                    }));
            }
            base.OnActionExecuting(context);
        }
    }
}