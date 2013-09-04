using IL.Examples.Patterns.Logic;
using IL.Examples.Patterns.Model;
using IL.Examples.Patterns.Model.Repositories;
using IL.Examples.Patterns.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;

namespace IL.Examples.Patterns.WebApplication.Services
{                                       
    public class AuthenticationService
    {
        private UserService _userService;
        private IUserRepository _userRepository;

        public AuthenticationService(UserService userService, IUserRepository userRepository) 
        {
            this._userService = userService;
            this._userRepository = userRepository;
        }

        public string Authenticate(LoginModel model, HttpRequestMessage request)
        {        
            User usr = this._userService.Authenticate(model.Name, model.Password);  

            if (usr == null) 
                throw new Exception(@"Der Benutzer konnte nicht authentifiziert werden! 
                                    Bitte überprüfen Sie den Benutzernamen oder Passwort!");

            string clientIP = this.GetClientIp(request);
            return this.CreateToken(model.Name, clientIP);
        }

        public string CreateToken(string userName, string clientIP) 
        {
            return (userName + "_" + clientIP).Encrypt();
        }

        public bool ValidateToken(string token, string clientIP) 
        {
            string decryptedToken = token.Decrypt();
            string[] tokenValues = decryptedToken.Split('_');

            string name = tokenValues[0];
            string ip = tokenValues[1];

            if (ip.Equals(clientIP)) 
            {
                User user = this._userRepository.GetByName(name);
                if (user != null)
                    return true;
            }

            return false;
        }

        public string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            return null;
        }
    }
}