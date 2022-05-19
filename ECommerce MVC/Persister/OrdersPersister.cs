using ECommerce_MVC.Models;
using System.Data.SqlClient;

namespace ECommerce_MVC.Persister
{
    public class OrdersPersister
    {
        private readonly string ConnectionString;
        public OrdersPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Add(OrdersModel order)
        {
            var sql = @"insert into [dbo][Orders]
                      ([IdCust],[C8],[Price],[Date])
                      values
                      (@IdCust,@C8,@Price,@Date);
                      select @@identity as 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCust", order.IdCust);
            command.Parameters.AddWithValue("@C8", order.C8);
            command.Parameters.AddWithValue("@Price", order.Price);
            command.Parameters.AddWithValue("@Date", order.Date);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public OrdersModel GetOrder(int IdOrder)
        {
            var sql = @"select
                      o.IdOrder, o.IdCust, o.C8, o.Price, o.Date
                      from Orders o
                      where o.IdOrder = @IdOrder";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdOrder", IdOrder);
            var reader = command.ExecuteReader();
            OrdersModel result = null;
            while (reader.Read())
            {
                result = new OrdersModel
                {
                    IdOrder = Convert.ToInt32(reader["IdOrder"]),
                    IdCust = Convert.ToInt32(reader["IdCust"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    C8 = Convert.ToInt32(reader["C8"]),
                    Date = Convert.ToDateTime(reader["Date"]),

                };

            }
            return result;
        }

        public bool Update(OrdersModel order)
        {
            var sql = @"update [dbo].[Orders]
                      set [IdOrder]=@IdOrder, [IdCust]=@IdCust,[C8]=@C8, [Price]=@Price, [Date]=@Date,
                      where @IdOrder=IdOrder";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            command.Parameters.AddWithValue("@C8", order.C8);
            command.Parameters.AddWithValue("@IdCust", order.IdCust);
            command.Parameters.AddWithValue("@Price", order.Price);
            command.Parameters.AddWithValue("@Date", order.Date);
            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int IdOrder)
        {
            var sql = @"DELETE FROM [dbo].[Orders]
                        WHERE IdOrder=@IdOrder ";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdOrder", 1);
            return command.ExecuteNonQuery() > 0;
        }
    }
}
