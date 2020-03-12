using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public interface ICountryRepository
    {
        IEnumerable<Country> Countries { get; }

        Country GetCountryById(string countryId);
    }
}
