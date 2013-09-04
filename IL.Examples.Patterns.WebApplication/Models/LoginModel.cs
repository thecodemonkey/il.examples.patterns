using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security;

namespace IL.Examples.Patterns.WebApplication.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
