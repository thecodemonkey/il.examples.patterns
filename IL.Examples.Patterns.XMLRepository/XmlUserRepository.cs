using Bender;
using IL.Examples.Patterns.Model;
using IL.Examples.Patterns.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IL.Examples.Patterns.XMLRepository
{
    public class XmlUserRepository : IUserRepository
    {
        private static object lockThis = new object(); 
        private string _rootPath;


        public XmlUserRepository(object rootPath) 
        {
            this._rootPath = rootPath.ToString();
        }

        public IEnumerable<User> GetAll()
        {
            return this.LoadUsers();
        }

        public User GetByID(int id)
        {
            var users = this.LoadUsers();
            return users.Where(u => u.ID == id).FirstOrDefault();
        }

        public User GetByName(string name)
        {
            var users = this.LoadUsers();
            return users.Where(u => u.Name == name).FirstOrDefault();                
        }

        public int Save(User user)
        {
            var users = this.LoadUsers();

            if (user.ID.HasValue)
            {
                var usr = users.Where(u => u.ID == user.ID).FirstOrDefault();
                usr.Name = user.Name;
                usr.Password = user.Password;
                usr.Contact = user.Contact;
            }
            else 
            {
                user.ID = (from u in users orderby u.ID descending select u.ID).FirstOrDefault() + 1;
                users.Add(user);
            }

            this.SaveUser(users);

            return user.ID.Value;
        }

        public void Delete(int userID)
        {
            var users = this.LoadUsers();

            if (users.Remove(users.First(u => u.ID == userID))) 
            {
                this.SaveUser(users);
            }
        }

        private IList<User> LoadUsers() 
        {
            lock (lockThis)
            {
                User[] users = Deserialize.XmlFile<User[]>(this.FileName);
                return users.ToList();
            }
        }

        private void SaveUser(IList<User> users) 
        {
            lock (lockThis)
            {
                Serialize.Xml(users, this.FileName);
            }
        }

        private string FileName 
        {
            get 
            {
                return Path.Combine(this._rootPath, "Users.xml");
            }
        }

    }
}
