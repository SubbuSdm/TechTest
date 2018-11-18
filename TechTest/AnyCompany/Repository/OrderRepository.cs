using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT)", connection);

                command.Parameters.AddWithValue("@OrderId", order.Id);
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Order> GetOrders(Customer customer)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT Id, Amount, VAT FROM Order WHERE CustomerId = @CustomerId", connection);
                command.Parameters.AddWithValue("@CustomerId", customer.Id);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var order = new Order();
                    order.Id = Convert.ToInt32(reader["Id"]);
                    order.Amount = Convert.ToDouble(reader["Amount"]);
                    order.VAT = Convert.ToDouble(reader["VAT"]);
                    order.Customer = customer;
                    orders.Add(order);
                }
            }

            return orders;
        }
    }
}
