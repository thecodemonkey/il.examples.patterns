using System;                               
using System.Collections.Generic;       
using System.Linq;                  
using System.Text;             
using System.Threading.Tasks;

namespace IL.Examples.Patterns.Model.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByID(int id);
        User GetByName(string name);
        int Save(User user);
        void Delete(int userID);
    }
}
