using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.DataLayer
{
    public interface ISportsProUnit
    {
        Repository<Product> Products { get; }
        Repository<Country> Countries { get; }
        Repository<Customer> Customers { get; }
        Repository<Incident> Incidents { get; }
        Repository<Registration> Registrations { get; }
        Repository<Technician> Technicians { get; }

        void Save();

    }
}
