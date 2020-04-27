using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using CSC237_ahrechka_SportsStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Components
{
    public class CountryDropDownOptions : ViewComponent
    {
        public IRepository<Country> data { get; set; }
        public CountryDropDownOptions(IRepository<Country> rep)
        {
            data = rep;
        }

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var countries = data.List(new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            });

            var vm = new DropDownOptionsViewModel
            {
                SelectedValue = value,
                DefaultText = defaultText,
                DefaultValue = defaultValue,
                Items = countries.ToDictionary(c => c.CountryID.ToString(),
                                            c => c.Name)
            };

            return View(DropDownOptions.PartialViewPath, vm);
        }
    }
}
