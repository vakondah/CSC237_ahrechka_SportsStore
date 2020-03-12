using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public class MockTechnicianRepository : ITechnicianRepository
    {
        public IEnumerable<Technician> GetTechnicians =>
            new List<Technician>
            {
                new Technician
                {
                    TechnicianID = 11,
                    Name = "Alison Diaz",
                    Email = "alison@sportsprosoftware.com",
                    Phone = "800-555-0443"
                },
                new Technician
                {
                    TechnicianID = 12,
                    Name = "Jason Lee",
                    Email = "jason@sportsprosoftware.com",
                    Phone = "800-555-0444"
                },
                new Technician
                {
                    TechnicianID = 13,
                    Name = "Andrew Wilson",
                    Email = "awilson@sportsprosoftware.com",
                    Phone = "800-555-0449"
                },
                new Technician
                {
                    TechnicianID = 14,
                    Name = "Gunter Wendt",
                    Email = "gunter@sportsprosoftware.com",
                    Phone = "800-555-0400"
                },
                new Technician
                {
                    TechnicianID = 15,
                    Name = "Gina Fiori",
                    Email = "gfiori@sportsprosoftware.com",
                    Phone = "800-555-0459"
                }
            };

        public Technician GetTechnicianById(int technicianId)
        {
            return GetTechnicians.FirstOrDefault(t => t.TechnicianID == technicianId);
        }
    }
}
