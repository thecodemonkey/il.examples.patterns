using IL.Examples.Patterns.Model;                   
using IL.Examples.Patterns.Model.Repositories;
using System;                              
using System.Collections.Generic;         
using System.Linq;                      
using System.Security.Authentication;
using System.Text;               
using System.Threading.Tasks;

namespace IL.Examples.Patterns.Logic
{                                       
    public class UserService                
    {                                               
        private IUserRepository _userRepository;            

        public UserService(IUserRepository userRepository) 
        {
            this._userRepository = userRepository;
        }

        public User Authenticate(string userName, string password) 
        {
            User user = _userRepository.GetByName(userName);

            if (user != null)
            {
                if (user.IsPasswordEncrypted() &&
                    password.EncryptMD5().Equals(user.Password)) 
                {
                    return user;
                }
            }

            throw new AuthenticationException("invalid userName or password!");
        }

        public User Register(string userName, string password) 
        {
            User user = new User {
                Name = userName,
                Password = password.EncryptMD5()
            };
            int id = this._userRepository.Save(user);

            return this._userRepository.GetByID(id);
        }
    }
}
