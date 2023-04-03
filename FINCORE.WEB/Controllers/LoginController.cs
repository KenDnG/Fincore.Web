using FINCORE.Models.Models.Masters;
using FINCORE.Models.Models.Users;
using FINCORE.Services.Helpers.Models.LDAP;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.LDAP;
using FINCORE.Services.Services.Masters;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using static FINCORE.Services.Helpers.Response.Collection;

namespace FINCORE.WEB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var s = Sessions.GetSessionFromJson<LDAPResponse>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                var b = Sessions.GetSessionFromJson<LDAPResponse>(HttpContext.Session, SessionIdentity.BRANCH_ID);
                if (s != null && b != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch { }

            string _status = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.EXPIRED_SESSION_STATUS);
            string _messages = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.EXPIRED_SESSION_MESSAGES);
			ViewBag.State = _status;
			ViewBag.Message = _messages;
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] UsersModels users)
        {
            APIResponse result = new APIResponse();
            string controllerName = "", actionName = "";
            var ldap = new LDAPServices();

            var userLdap = ldap.GetUserLDAP(users.Username, users.Password, users.CompanyId).Result;

            if (userLdap.err_code == Collection.Codes.SUCCESS)
            {
                var companyBranch = new CompanyBranchServices();
                var companyBranchAPI = companyBranch.GetCompanyBranch(users.CompanyId, userLdap.data.EmployeeId).Result;
                List<CompanyBranchResponse> res = new List<CompanyBranchResponse>();
                if (companyBranchAPI.status == "Success")
                {
                    res = JsonConvert.DeserializeObject<List<CompanyBranchResponse>>(companyBranchAPI.data.ToString());
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL, res.FirstOrDefault());
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.USER_CREDENTIAL, users);
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLdap.data.SamAccountName),
                        new Claim("FullName", userLdap.data.DisplayName),
                        new Claim(ClaimTypes.Role, "Administrator"),
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
                    // The time at which the authentication ticket expires. A
                    // value set here overrides the ExpireTimeSpan option of
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = < DateTimeOffset >,
                    // The time at which the authentication ticket was issued.

                    RedirectUri = "www.google.com"
                    // The full path or absolute URI to be used as an http
                    // redirect response value.
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                result = new APIResponse()
                {
                    code = Collection.Codes.SUCCESS,
                    data = res
                };
            }
            else
            {
                ViewBag.State = userLdap.status;
                ViewBag.Message = userLdap.err_message;
                result = new APIResponse()
                {
                    code = userLdap.err_code,
                    message = userLdap.err_message
                };
            }

            Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME, userLdap.data);

            return Json(result);
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            Sessions.RemoveAllSession(HttpContext.Session);
            return RedirectToAction("Index", "Login");
        }


		[HttpGet]
		public ActionResult ExpiredSession()
		{
			Sessions.RemoveAllSession(HttpContext.Session);

			Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.EXPIRED_SESSION_STATUS, Collection.Status.FAILED);
			Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.EXPIRED_SESSION_MESSAGES, "Your Login Session was EXPIRED !! Please Relogin");

			return RedirectToAction("Index", "Login");
		}

		public IActionResult PageOff()
        {
            return View("~/Views/Login/Index.cshtml");
            //return RedirectToAction("Index", "Login");
        }
        
    }
}