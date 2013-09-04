using IL.Examples.Patterns.Model;               
using IL.Examples.Patterns.Model.Repositories;
using System;                           
using System.Collections.Generic;     
using System.Data.SqlClient;       
using System.Linq;              
using System.Text;            
using System.Threading.Tasks;

namespace IL.Examples.Patterns.SQLRepository
{
    public class UserRepository : SQLRepositoryBase, IUserRepository
    {
        private static string BASIC_QUERY = @"select u.ID, u.Name, u.Password, 
                                                     c.ID as ContactID, c.FirstName, c.LastName, c.EMail from Users u
                                              left outer join Contacts c on (c.ID = u.Contact_ID)";

        public UserRepository(string connectionString) : base(connectionString) { }

        public IEnumerable<User> GetAll()
        {
            return this.Read<User>(BASIC_QUERY, ReadUser);
        }

        public User GetByName(string name)
        {                                               
            return this.Read<User>(BASIC_QUERY + " where u.Name = '" + name + "'", ReadUser)
                       .FirstOrDefault();
        }

        public User GetByID(int id)
        {                               
            return this.Read<User>(BASIC_QUERY + "where u.ID = '" + id + "'", ReadUser)
                       .FirstOrDefault();
        }     

        public void Delete(int userID)
        {
            this.Execute("delete from Users where ID=" + userID);
        }

        public int Save(User user)
        {
            if (user.ID.HasValue)
                return Update(user);

            return this.Insert(user);
        }

        private int SaveContact(Contact contact) 
        {
            if (contact != null) 
            {
                if (contact.ID > 0)
                    return UpdateContact(contact);

                return this.InsertContact(contact);            
            }

            return -1;
        }

        private int Insert(User user)
        {
            int nContactID = this.SaveContact(user.Contact);
                
            return this.Execute(String.Format(
                "insert into Users (Name, Password, Contact_ID) values ('{0}', '{1}', {2}) select @@IDENTITY"
                , user.Name.Trim()
                , user.Password.Trim()
                , (nContactID > 0)? nContactID.ToString() : "Null"
            ));
        }

        private int Update(User user)
        {
            int nContactID = this.SaveContact(user.Contact);

            return this.Execute(String.Format(
                "update Users SET Name='{0}', Password='{1}', Contact_ID={2} WHERE ID = {3}"
                , user.Name.Trim()
                , user.Password.Trim()
                , (nContactID > 0) ? nContactID.ToString() : "Null"
                , user.ID
            ));
        }

        private int UpdateContact(Contact contact)
        {
            this.Execute(String.Format(
                "update Contacts SET FirstName='{0}', LastName='{1}', EMail = '{2}' WHERE ID = {3}"
                , contact.FirstName.Trim()
                , contact.LastName.Trim()
                , contact.EMail.Trim()
                , contact.ID
            ));

            return contact.ID;
        }

        private int InsertContact(Contact contact)
        {
            return this.Execute(String.Format(
                "insert into Contacts (FirstName, LastName, EMail) values ('{0}', '{1}', '{2}') select @@IDENTITY"
                , contact.FirstName.Trim()
                , contact.LastName.Trim()
                , contact.EMail.Trim()
            ));
        }

        private User ReadUser(SqlDataReader reader) 
        {

            int cID = reader.IsDBNull(reader.GetOrdinal("ContactID")) ? -1 : 
                                      reader.GetInt32(reader.GetOrdinal("ContactID"));

            return new User
            {
                ID = (int)reader["ID"],
                Name = reader["Name"].ToString().Trim(),
                Password = reader["Password"].ToString().Trim(),

                Contact = (cID > -1)? this.ReadContact(reader, cID) : null
            };        
        }

        private Contact ReadContact(SqlDataReader reader, int contactID)
        {
            return new Contact
            {
                ID = contactID,
                FirstName = reader["FirstName"].ToString().Trim(),
                LastName = reader["LastName"].ToString().Trim(),
                EMail = reader["EMail"].ToString().Trim()
            };
        }

    }
}
