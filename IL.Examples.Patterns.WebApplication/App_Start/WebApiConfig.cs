using IL.Examples.Patterns.WebApplication.App_Start;
using IL.Examples.Patterns.WebApplication.Filters;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IL.Examples.Patterns.WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            HttpTokenAuthorizationFilter authFilter = UnityConfig.GetConfiguredContainer()
                                                                 .Resolve<HttpTokenAuthorizationFilter>();
            //new AuthorizeAttribute()
            config.Filters.Add(authFilter);
        }
    }
}