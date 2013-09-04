using IL.Examples.Patterns.Logic;
using IL.Examples.Patterns.WebApplication.Models;
using IL.Examples.Patterns.WebApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace IL.Examples.Patterns.WebApplication.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private AuthenticationService _authenticationService;

        public LoginController(AuthenticationService authenticationService) 
        {
            this._authenticationService = authenticationService;
        }

        public HttpResponseMessage Post(LoginModel loginModel) 
        {
            try
            {
                string token = this._authenticationService.Authenticate(loginModel, this.Request);
                FormsAuthentication.SetAuthCookie(loginModel.Name, loginModel.RememberMe);

                return this.Request.CreateResponse(HttpStatusCode.OK, new { token = token });
            }
            catch(Exception exp)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, exp);
            }
        }
    }
}
