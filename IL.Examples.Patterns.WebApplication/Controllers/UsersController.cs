using IL.Examples.Patterns.Logic;
using IL.Examples.Patterns.Model;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Omu.ValueInjecter;

namespace IL.Examples.Patterns.WebApplication.Controllers
{
    public class UsersController : ApiController
    {
        public IUserRepository _userRepository;
        public UserService _userService;

        public UsersController(IUserRepository userRepository, UserService userService) 
        {                                                     
            this._userRepository = userRepository;
            this._userService = userService;
        }           

        public IEnumerable<UserModel> Get()                  
        {
            var users = this._userRepository.GetAll();

            return users.Select(x => new UserModel().InjectFrom<FlatLoopValueInjection>(x)).Cast<UserModel>();
        }       

        public UserModel Get(int id)
        {                               
            var user = this._userRepository.GetByID(id);

            return (new UserModel()).InjectFrom<FlatLoopValueInjection>(user) as UserModel;
        }

        public HttpResponseMessage Post(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User().InjectFrom<UnflatLoopValueInjection>(userModel) as User;

                if (user.ID.HasValue)
                    this._userRepository.Save(user);
                else
                    this._userService.Register(user.Name, user.Password);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else 
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        public void Delete(int id) 
        {
            this._userRepository.Delete(id);
        }
    }
}
