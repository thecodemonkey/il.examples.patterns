using IL.Examples.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.EFRepository
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Default") 
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
