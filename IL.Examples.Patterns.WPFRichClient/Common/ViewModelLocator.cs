using IL.Examples.Patterns.WPFRichClient.ViewModels;
using Microsoft.Practices.Unity;                
using System;                       
using System.Collections.Generic;
using System.Linq;              
using System.Text;            
using System.Threading.Tasks;

namespace IL.Examples.Patterns.WPFRichClient.Common
{
    public class ViewModelLocator
    {
        private static IUnityContainer _diContainer;

        static ViewModelLocator() 
        {                               
            _diContainer = UnityConfig.Register();
        }

        public static EditorViewModel EditorVM 
        { 
            get
            {
                return _diContainer.Resolve<EditorViewModel>();
            }
        }

        public static LoginViewModel LoginVM 
        {
            get
            {
                return _diContainer.Resolve<LoginViewModel>();
            }
        }

        public static MainViewModel MainVM 
        {           
            get
            {
                return _diContainer.Resolve<MainViewModel>();
            }
        }

        public static UsersViewModel UsersVM 
        {
            get
            {
                return _diContainer.Resolve<UsersViewModel>();
            }
        } 
    }
}
