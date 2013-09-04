using IL.Examples.Patterns.Model;                       
using IL.Examples.Patterns.Model.Repositories;      
using System;
using System.Data.Entity;
using System.Collections.Generic;       
using System.Linq;                  
using System.Text;              
using System.Threading.Tasks;

namespace IL.Examples.Patterns.EFRepository
{
    public class EFUserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            using (EFContext context = new EFContext()) 
            {
                return context.Users.Include(c => c.Contact).ToList();
            }
        }

        public User GetByID(int id)
        {
            using (EFContext context = new EFContext())
            {
                return context.Users.Include(c => c.Contact).Where(u => u.ID == id).FirstOrDefault();
            }
        }

        public User GetByName(string name)
        {
            using (EFContext context = new EFContext())
            {
                return context.Users.Include(c => c.Contact).Where(u => u.Name == name).FirstOrDefault();
            }
        }

        public int Save(User user)
        {
            User usr = null;

            using (EFContext context = new EFContext())
            {
                if (!user.ID.HasValue)
                     usr = context.Users.Add(user);
                else 
                {
                    if (user.Contact != null) 
                    {
                        if (user.Contact.ID > 0)
                            context.Entry<Contact>(user.Contact).State = System.Data.EntityState.Modified;
                        else
                            context.Contacts.Add(user.Contact);
                    }

                    context.Entry<User>(user).State = System.Data.EntityState.Modified;
                    usr = user;
                }

                //if (user.Contact != null) 
                //{ 
                //    if (user.Contact.ID > 0)                
                //}

                context.SaveChanges();

                return usr.ID.Value;
            }
        }

        public void Delete(int userID)
        {
            using (EFContext context = new EFContext())
            {
                //var user = context.
                context.Entry<User>(new User { ID = userID }).State = System.Data.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
