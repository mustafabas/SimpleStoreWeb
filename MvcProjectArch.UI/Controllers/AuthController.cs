using MvcProjectArch.Services.Users;
using MvcProjectArch.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
   [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Admin
        IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult login(string returnUrl)
        {
            var model = new LoginModel { ReturnUrl = returnUrl };
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            var anyUser = _userService.GetAllUser().FirstOrDefault(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password);
           
            if(anyUser!=null)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, anyUser.Name+" "+anyUser.Surname), new Claim(ClaimTypes.Email, anyUser.Email), new Claim(ClaimTypes.NameIdentifier, anyUser.ID.ToString())}, "ApplicationCookie");
                
                identity.AddClaim(new Claim(ClaimTypes.Role, Convert.ToString((byte)UserType.Admin)));

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);
               var redirect= GetRedirectUrl(loginModel.ReturnUrl);
               return Redirect(redirect);
            }
            else
            {
                var anyCustomer = _userService.GetAllCustomer().Where(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password).FirstOrDefault();
                if (anyCustomer != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, anyCustomer.NameSurname), new Claim(ClaimTypes.Email, anyCustomer.Email), new Claim(ClaimTypes.NameIdentifier, anyCustomer.ID.ToString()) }, "ApplicationCookie");

                    identity.AddClaim(new Claim(ClaimTypes.Role, Convert.ToString((byte)UserType.Customer)));

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    var redirect = GetRedirectUrl(loginModel.ReturnUrl);
                    return Redirect(redirect);
                }
                else
                {
                    loginModel.ErrorMessage = "Kullanıcı Adı veya Şifre Hatalı";
                }
              
            }
            return View(loginModel);
        }
       public ActionResult test()
        {
            return View();
        }
       private string GetRedirectUrl(string returnUrl)
       {
           if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
           {
               return Url.Action("index", "home");
           }
           return returnUrl;
       }
       [AllowAnonymous]
       public ActionResult LogOut()
       {
           var ctx = Request.GetOwinContext();
           var authManager = ctx.Authentication;

           authManager.SignOut("ApplicationCookie");
           return RedirectToAction("index", "home");
       }
    }
}