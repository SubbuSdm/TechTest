using System;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = this.customerRepository.GetCustomer(customerId);

            if (customer == null)
                throw new ArgumentNullException(string.Format("Customer details not found for customer id {0}", customerId));

            if (order.Amount == 0)
                return false;

            // Enriching the request 
            order = OrderRequestFactory.GenerateOrderRequest(order, customer.Country);

            this.orderRepository.Save(order);
            return true;
        }
        
    }
}
