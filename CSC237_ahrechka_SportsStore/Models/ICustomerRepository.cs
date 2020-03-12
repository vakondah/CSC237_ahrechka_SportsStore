using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers { get; }

        Customer GetCustomerById(int customerId);
    }
}
