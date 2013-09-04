using IL.Examples.Patterns.EFRepository;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.XMLRepository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.Console
{
    public class UnityConfig
    {
        public static IUnityContainer Register() 
        {
            var container = new UnityContainer();

            string rootPath = ConfigurationManager.AppSettings["dataPath"];

            //container.RegisterType<IUserRepository, XmlUserRepository>(
            //    new InjectionConstructor(rootPath));

            container.RegisterType<IUserRepository, EFUserRepository>();

            return container;
        }
    }
}
