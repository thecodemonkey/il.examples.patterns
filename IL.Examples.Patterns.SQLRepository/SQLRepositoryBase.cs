using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.SQLRepository
{
    public class SQLRepositoryBase
    {
        private string _connectionString;

        protected SQLRepositoryBase(string connectionString) 
        {
            this._connectionString = connectionString;
        }

        protected List<TEntity> Read<TEntity>(string query, Func<SqlDataReader, TEntity> action) where TEntity : new()
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<TEntity> entities = new List<TEntity>();
                        while (reader.Read())
                        {
                            entities.Add(action(reader));
                        }
                        return entities;
                    }
                }
            }
        }

        protected int Execute(string query)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var id = command.ExecuteScalar();
                    return Convert.ToInt32(id);
                }
            }
        }

    }
}
