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
    public class TechnicianDropDownOptions : ViewComponent
    {
        public IRepository<Technician> data { get; set; }
        public TechnicianDropDownOptions(IRepository<Technician> rep)
        {
            data = rep;
        }

        public IViewComponentResult Invoke(string value, string defaultText, string defaultValue)
        {
            var techs = data.List(new QueryOptions<Technician>
            {
                OrderBy = t => t.Name
            });

            var vm = new DropDownOptionsViewModel
            {
                SelectedValue = value,
                DefaultText = defaultText,
                DefaultValue = defaultValue,
                Items = techs.ToDictionary(t => t.TechnicianID.ToString(),
                                            t => t.Name)
            };

            return View(DropDownOptions.PartialViewPath, vm);
        }
    }
}
