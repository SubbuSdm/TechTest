using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    class ProxyCustomerRepository : ICustomerRepository
    {
        Customer ICustomerRepository.GetCustomer(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        IEnumerable<Customer> ICustomerRepository.GetCustomers()
        {
            return CustomerRepository.GetCustomers();
        }
    }
}
