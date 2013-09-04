using IL.Examples.Patterns.WebApplication.Services;
using System;                               
using System.Collections.Generic;           
using System.Linq;                      
using System.Net;
using System.Net.Http;
using System.Security.Principal;    
using System.Web;                   
using System.Web.Http;              
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IL.Examples.Patterns.WebApplication.Filters
{
    public class HttpTokenAuthorizationFilter : AuthorizationFilterAttribute
    {
        private AuthenticationService _authenticationService;

        public HttpTokenAuthorizationFilter(AuthenticationService authenticationService) 
        {
            this._authenticationService = authenticationService;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!this.AllowAnonymous(actionContext.ActionDescriptor))
            {
                if (actionContext.Request.Headers.Contains("X-Token"))
                {
                    string encryptedToken = actionContext.Request.Headers.GetValues("X-Token").First();

                    try
                    {
                        string clientIP = this._authenticationService.GetClientIp(actionContext.Request);

                        if (!this._authenticationService.ValidateToken(encryptedToken, clientIP)) 
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid identity or client machine.");
                        }
                    }
                    catch (Exception ex)
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Request is missing authorization token.");
                }
            }


            base.OnAuthorization(actionContext);
        }

        private bool AllowAnonymous(HttpActionDescriptor actionDescriptor)
        {
            bool allow = false;

            if (actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().FirstOrDefault() != null)
            {
                allow = true;
            }
            else if (actionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().FirstOrDefault() != null)
            {
                allow = true;
            }

            return allow;
        }
    }
}