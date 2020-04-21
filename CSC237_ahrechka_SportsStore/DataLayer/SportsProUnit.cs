using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.DataLayer
{
    public class SportsProUnit : ISportsProUnit
    {

        private SportsProContext context { get; set; }
        public SportsProUnit(SportsProContext ctx) => context = ctx;

        private IRepository<Product> productRepo;
        public IRepository<Product> Products
        {
            get
            {
                if (productRepo == null)
                    productRepo = new Repository<Product>(context);
                    return productRepo;
            }
        }

        private IRepository<Country> countryRepo;
        public IRepository<Country> Countries
        {
            get
            {
                if (countryRepo == null)
                    countryRepo = new Repository<Country>(context);
                return countryRepo;
            }
        }

        private IRepository<Customer> customerRepo;
        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepo == null)
                    customerRepo = new Repository<Customer>(context);
                return customerRepo;
            }
        }

        private IRepository<Incident> incidentRepo;
        public IRepository<Incident> Incidents
        {
            get
            {
                if (incidentRepo == null)
                    incidentRepo = new Repository<Incident>(context);
                return incidentRepo;
            }
        }

        private IRepository<Registration> registrationRepo;
        public IRepository<Registration> Registrations
        {
            get
            {
                if (registrationRepo == null)
                    registrationRepo = new Repository<Registration>(context);
                return registrationRepo;
            }
        }

        private IRepository<Technician> technicianRepo;
        public IRepository<Technician> Technicians
        {
            get
            {
                if (technicianRepo == null)
                    technicianRepo = new Repository<Technician>(context);
                return technicianRepo;
            }
        }

        public void Save() => context.SaveChanges();
        
    }
}
