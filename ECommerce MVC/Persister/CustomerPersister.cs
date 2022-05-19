using ECommerce_MVC.Models;
using System.Data.SqlClient;

namespace ECommerce_MVC.Persister
{
    public class CustomerPersister
    {
        private readonly string ConnectionString;
        public CustomerPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Add(CustomerModel model)
        {
            var sql = @"insert into [dbo][Customer]
                      ([mail],[Name],[Surname],[Birth])
                      values
                      (@mail,@Name,@Surname,@Birth);
                      select @@identity as 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@mail", model.Mail);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Surname", model.Surname);
            command.Parameters.AddWithValue("@Birth", model.Birth);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public CustomerModel GetCustomer(int IdCust)
        {
            var sql = @"select
                      c.IdCust, c.mail, c.Name, c.Surname, c.Birth
                      from Customer c
                      where c.IdCust = @IdCust";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdCust", IdCust);
            var reader = command.ExecuteReader();
            CustomerModel result = null;
            while (reader.Read())
            {
                result = new CustomerModel
                {
                    IdCust = Convert.ToInt32(reader["IdCust"]),
                    Mail = reader["mail"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    Birth = Convert.ToDateTime(reader["Birth"]),

                };

            }
            return result;
        }

        public bool Update(CustomerModel customer)
        {
            var sql = @"update [dbo].[Customer]
                      set [IdCust]=@IdCust, [mail]=@Mail,[Name]=@Name, [Surname]=@Surname, [Birth]=@Birth,
                      where @IdCust=IdCust";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCust", customer.IdCust);
            command.Parameters.AddWithValue("@Mail", customer.Mail);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Surname", customer.Surname);
            command.Parameters.AddWithValue("@Birth", customer.Birth);
            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int IdCust)
        {
            var sql = @"DELETE FROM [dbo].[Customer]
                        WHERE IdCust=@IdCust";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCust", 1);
            return command.ExecuteNonQuery() > 0;
        }
    }
}
