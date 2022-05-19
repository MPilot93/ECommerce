using ECommerce_MVC.Models;
using System.Data.SqlClient;

namespace ECommerce_MVC.Persister
{
    public class PricesPersister
    {
        private readonly string ConnectionString;
        public PricesPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Add(PricesModel prices)
        {
            var sql = @"insert into [dbo][Prices]
                      ([C8],[Price],[Country],[Currency])
                      values
                      (@C8,@Price,@Country,@Currency);
                      select @@identity as 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@C8", prices.C8);
            command.Parameters.AddWithValue("@Price", prices.Price);
            command.Parameters.AddWithValue("@Country", prices.Country);
            command.Parameters.AddWithValue("@Currency", prices.Currency);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public PricesModel GetPrices(int C8)
        {
            var sql = @"select
                      p.Cod, p.C8, p.Price, p.Country, p.Currency
                      from Prices p
                      where p.C8 = @C8";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@C8", C8);
            var reader = command.ExecuteReader();
            PricesModel result = null;
            while (reader.Read())
            {
                result = new PricesModel
                {
                    Cod = Convert.ToInt32(reader["Cod"]),
                    C8 = Convert.ToInt32(reader["C8"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Country = reader["Country"].ToString(),
                    Currency = reader["Currency"].ToString(),
                    
                };

            }
            return result;
        }

        public bool Update(PricesModel prices)
        {
            var sql = @"update [dbo].[Prices]
                      set [Cod]=@Cod, [C8]=@C8,[Price]=@Price, [Country]=@Country, [Currency]=@Currency,
                      where @Cod=Cod";

            using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cod", prices.Cod);
                command.Parameters.AddWithValue("@C8", prices.C8);
                command.Parameters.AddWithValue("@Price", prices.Price);
                command.Parameters.AddWithValue("@Country", prices.Country);
                command.Parameters.AddWithValue("@Currency", prices.Currency);
                return command.ExecuteNonQuery() > 0; 
        }

        public bool Delete(int Cod)
        {
            var sql = @"DELETE FROM [dbo].[Price]
                        WHERE Cod=@Cod ";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Cod", 1);
            return command.ExecuteNonQuery() > 0;
        }
    }
}
