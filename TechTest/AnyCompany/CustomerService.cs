using System.Collections.Generic;

namespace AnyCompany
{
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
     
        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }

        public IEnumerable<CustomerOrder> GetCustomersWithOrders()
        {
            var customerWithOrders = new List<CustomerOrder>();

            var customers = this.customerRepository.GetCustomers();

            foreach (var customer in customers)
            {
                var orders = this.orderRepository.GetOrders(customer);

                customerWithOrders.Add(new CustomerOrder() { Customer = customer, Orders = orders });
            }

            return customerWithOrders;
        }
    }
}
