using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    public class OrderServiceTest
    {
        [Test]
        public void GivenOrderWithExistingCustomerWhenCallServiceThenOrderProcessedSuccessfully()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            
            var orderService = new OrderService(orderRepository.Object, customerRepository.Object);
            
            var order = new Order() { Amount = 100 };
            var customer = new Customer() { Id = 1, Name = "Mark", Country = "UK" };

            orderRepository.Setup(r => r.Save(It.IsAny<Order>()));
            customerRepository.Setup(r => r.GetCustomer(It.IsAny<Int32>())).Returns(customer);

            var success = orderService.PlaceOrder(order, 1);

            Assert.IsTrue(success);
        }

        [Test]
        public void GivenOrderWithZeroAmountWhenCallServiceThenOrderProcessFailed()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

            var orderService = new OrderService(orderRepository.Object, customerRepository.Object);

            var order = new Order() { Amount = 0 };
            var customer = new Customer() { Id = 1, Name = "Mark", Country = "UK" };

            orderRepository.Setup(r => r.Save(It.IsAny<Order>()));
            customerRepository.Setup(r => r.GetCustomer(It.IsAny<Int32>())).Returns(customer);

            var success = orderService.PlaceOrder(order, 1);

            Assert.IsFalse(success);
        }
    }
}
