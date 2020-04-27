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
    public class CustomerDropDownOptions : ViewComponent
    {
        public IRepository<Customer> data { get; set; }
        public CustomerDropDownOptions(IRepository<Customer> rep)
        {
            data = rep;
        }

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var customers = data.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.FirstName
            });

            var vm = new DropDownOptionsViewModel
            {
                SelectedValue = value,
                DefaultText = defaultText,
                DefaultValue = defaultValue,
                Items = customers.ToDictionary(p => p.CustomerID.ToString(),
                                            c => c.FullName)
            };

            return View(DropDownOptions.PartialViewPath, vm);
        }
    }
}
