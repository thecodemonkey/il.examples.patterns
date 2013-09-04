using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.SQLRepository;
using System.Configuration;
using IL.Examples.Patterns.XMLRepository;
using System.Web;
using System.IO;
using System.Collections.Generic;
using IL.Examples.Patterns.EFRepository;

namespace IL.Examples.Patterns.WebApplication.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            string connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString; 
            //container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(connection));
            container.RegisterType<IUserRepository, XmlUserRepository>(new InjectionConstructor(container.Resolve<RootPath>()));

            //container.RegisterType<IUserRepository, EFUserRepository>();
        }

        private class RootPath : Object 
        {
            public override string ToString()
            {
                return HttpContext.Current.Server.MapPath("~/App_Data");
            }
        }
    }
}
