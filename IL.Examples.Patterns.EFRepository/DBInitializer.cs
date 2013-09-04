using IL.Examples.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.EFRepository
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            User user = new User { Name = "admin", Password = "admin".EncryptMD5() };
            user.Contact = new Contact { FirstName = "Spongebob", LastName = "Schwammkopf", EMail = "bikini@bottom.com" };

            context.Users.Add(user);
            context.SaveChanges(); 
             
            base.Seed(context);
        }
    }
}
