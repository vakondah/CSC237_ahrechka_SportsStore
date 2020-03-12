using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public interface ITechnicianRepository
    {
        IEnumerable<Technician> GetTechnicians { get; }

        Technician GetTechnicianById(int technicianId);
    }
}
