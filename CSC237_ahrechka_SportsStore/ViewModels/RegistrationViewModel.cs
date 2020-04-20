using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.ViewModels
{
    public class RegistrationViewModel
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Registration> Registrations { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
