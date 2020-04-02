using CSC237_ahrechka_SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.ViewModels
{
    public class IncidentListViewModel
    {
        public string Filter { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
