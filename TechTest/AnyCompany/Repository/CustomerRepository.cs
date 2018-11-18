using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId ", connection);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    customer.Country = reader["Country"].ToString();
                }
            }

            return customer;
        }

        public static IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT DISTINCT C.Name, C.Id CustomerId, C.Country FROM Customer C JOIN Order O ON O.CustomerId = C.Id Order by Name", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var customer = new Customer();
                    customer.Id = Convert.ToInt32(reader["CustomerId"]);
                    customer.Name = reader["Name"].ToString();
                    customer.Country = reader["Country"].ToString();

                    customers.Add(customer);
                }
            }

            return customers;
        }
    }
}
