using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.DataLayer
{
    public interface ISportsProUnit
    {
        IRepository<Product> Products { get; }
        IRepository<Country> Countries { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Incident> Incidents { get; }
        IRepository<Registration> Registrations { get; }
        IRepository<Technician> Technicians { get; }

        void Save();

    }
}
