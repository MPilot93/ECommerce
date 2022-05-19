using ECommerce_MVC.Models;
using System.Data.SqlClient;

namespace ECommerce_MVC.Persister
{
    public class ProductPersister
    {
        private readonly string ConnectionString;
        public ProductPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Add(ProductModel product)
        {
            var sql = @"insert into [dbo].[Product]
                      [C8], [Description], [Title], [UrlImg]
                      values 
                      @C8, @Description, @Title, @UrlImg);
                      select @@identity as 'identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@C8", product.C8);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Title", product.Title);
            command.Parameters.AddWithValue("@UrlImg", product.UrlImg);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public ProductModel GetModel(int C8)
        {
            var sql = @"select
                      p.Id, p.C8, p.Description, p.Title, p.UrlImg
                      from Product p
                      where p.C8=@C8";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@C8", C8);
            var reader = command.ExecuteReader();
            ProductModel result = null;
            while (reader.Read())
            {
                result = new ProductModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    C8 = Convert.ToInt32(reader["C8"]),
                    Description = Convert.ToString(reader["Description"]),
                    Title = Convert.ToString(reader["Title"]),
                    UrlImg = Convert.ToString(reader["Hours"]),
                };

            }
            return result;
        }
    }
}
