using System.Collections.Generic;

namespace AnyCompany
{
    public interface IOrderRepository
    {
        void Save(Order order);

        IEnumerable<Order> GetOrders(Customer customer);
    }
}