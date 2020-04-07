using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; }
        public Incident Incident { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
