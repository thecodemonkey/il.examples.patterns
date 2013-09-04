using IL.Examples.Patterns.Model;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.WPFRichClient.Common;
using System;                                
using System.Collections.Generic;       
using System.Collections.ObjectModel;
using System.Linq;                
using System.Text;              
using System.Threading.Tasks;

namespace IL.Examples.Patterns.WPFRichClient.ViewModels
{
    public class UsersViewModel : ViewModelBase<UsersViewModel>
    {
        private IUserRepository _userRepository;
        public ObservableCollection<User> Users{ get; set; }

        public UsersViewModel(IUserRepository userRepository) 
        {                                               
            this._userRepository = userRepository;

            this.Users = new ObservableCollection<User>(
                this._userRepository.GetAll()
            );
                
            //    new ObservableCollection<User>(
            //    new User[]{
            //        new User{ ID = 1, Name = "Jeff", Password = "hello!"},
            //        new User{ ID = 1, Name = "Hi", Password = "aa!"},
            //        new User{ ID = 1, Name = "Was", Password = "sdf!"},
            //        new User{ ID = 1, Name = "Here", Password = "ffff!"},
            //        new User{ ID = 1, Name = "Hihi", Password = "dd!"}
            //    }
            //);
            
        }
    }
}
