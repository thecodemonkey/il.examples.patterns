using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IL.Examples.Patterns.WebApplication.Models
{
    public class UserModel
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public int ContactID { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        [EmailAddress]
        public string ContactEMail { get; set; }
    }
}