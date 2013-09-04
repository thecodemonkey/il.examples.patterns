using IL.Examples.Patterns.WebApplication.Services;     
using System;                                   
using System.Collections.Generic;        
using System.Linq;              
using System.Web;         
using System.Web.Mvc;
using System.Web.Security;

namespace IL.Examples.Patterns.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private AuthenticationService _authenticationService;

        public HomeController(AuthenticationService authenticationService) 
        {
            this._authenticationService = authenticationService;        
        }

        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.ViewBag.XToken = this._authenticationService.CreateToken(this.User.Identity.Name,
                                                                            this.Request.UserHostAddress);
            }

            return View();
        }

        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return this.RedirectToAction("Index");
        }
    }
}
