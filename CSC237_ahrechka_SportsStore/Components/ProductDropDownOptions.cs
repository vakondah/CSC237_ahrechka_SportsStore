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
    public class ProductDropDownOptions : ViewComponent
    {
        public IRepository<Product> data { get; set; }
        public ProductDropDownOptions(IRepository<Product> rep)
        {
            data = rep;
        }

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var products = data.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            });

            var vm = new DropDownOptionsViewModel
            {
                SelectedValue = value,
                DefaultText = defaultText,
                DefaultValue = defaultValue,
                Items = products.ToDictionary(p => p.ProductID.ToString(),
                                            p => p.Name)
            };

            return View(DropDownOptions.PartialViewPath, vm);
        }
    }
}
