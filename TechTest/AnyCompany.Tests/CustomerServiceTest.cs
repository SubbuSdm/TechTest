using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    public class CustomerServiceTest
    {
        [Test]
        public void GivenCustomersAvailableWithOrdersWhenCallServiceThenReturnsCustomers()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

            var customerService = new CustomerService(customerRepository.Object, orderRepository.Object);
            
            var orders = new List<Order>()
                            { new Order() { Amount = 100, Customer = new Customer() { Id = 1, Name = "Mark", Country = "UK" } }
                            };

            var customersWithOrder = new List<Customer>()
                                { new Customer() { Id = 1, Name = "Mark", Country = "UK" }
                                };

            orderRepository.Setup(r => r.GetOrders(It.IsAny<Customer>())).Returns(orders);
            customerRepository.Setup(r => r.GetCustomers()).Returns(customersWithOrder);

            var customersWithOrders = customerService.GetCustomersWithOrders();

            Assert.IsTrue(customersWithOrders.Count() == 1);
        }

        [Test]
        public void GivenCustomersWithNoOrdersWhenCallServiceThenReturnsWithOnlyCustomers()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

            var customerService = new CustomerService(customerRepository.Object, orderRepository.Object);

            var customers = new List<Customer>()
                                { new Customer() { Id = 1, Name = "Mark", Country = "UK" } };

            var orders = new List<Order>() { };
            
            orderRepository.Setup(r => r.GetOrders(It.IsAny<Customer>())).Returns(orders);
            customerRepository.Setup(r => r.GetCustomers()).Returns(customers);

            var customersWithOrders = customerService.GetCustomersWithOrders();

            Assert.IsTrue(customersWithOrders.FirstOrDefault().Orders.Count() == 0);
        }
    }
}
