using IL.Examples.Patterns.EFRepository;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.SQLRepository;
using IL.Examples.Patterns.WPFRichClient.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.WPFRichClient.Common
{
    public class UnityConfig
    {
        public static IUnityContainer Register() 
        {
            string connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            IUnityContainer container = new UnityContainer();
            //container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(connection));
            container.RegisterType<IUserRepository, EFUserRepository>();


            return container;
        }
    }
}
