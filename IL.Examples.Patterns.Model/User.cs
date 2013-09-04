using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.Model
{
    public class User 
    {
        public int? ID { get; set; }
        
        public string Name { get; set; }
        public string Password { get; set; }

        public Contact Contact { get; set; }

        public bool IsPasswordEncrypted() 
        { 
            return Regex.IsMatch(this.Password, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
