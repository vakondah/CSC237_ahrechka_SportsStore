using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using CSC237_ahrechka_SportsStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class IncidentController: Controller
    {
        private SportsProUnit data { get; set; }
        public IncidentController(SportsProContext ctx)
        {
            data = new SportsProUnit(ctx);
        }

        [Route("[controller]s")]
        public IActionResult List(string filter = "all")
        {
            // To use view model create new instance of IncidentListViewModel:
            IncidentListViewModel model = new IncidentListViewModel()
            {
                Filter = filter
            };
            ViewBag.Title = "Incident List";

            var options = new QueryOptions<Incident>
            {
                Includes = "Customer, Product",
                OrderBy = i => i.DateOpened
            };

            if (filter == "unassigned")
            {
                options.Where = i => i.TechnicianID == null;
            }
            if (filter == "open")
            {
                options.Where = i => i.DateClosed == null;
            }

            IEnumerable<Incident> incidents = data.Incidents.List(options);
            model.Incidents = incidents;

            // pass model instead of List<Incident>:
            return View(model);
        }

        public IActionResult Filter(string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }

        // StoreListsInViewBag() was replased with 
        // GetViewModel()

        // helper method:
        private IncidentViewModel GetViewModel()
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Customers = data.Customers.List(new QueryOptions<Customer>
                {
                    OrderBy = c => c.FirstName
                }),

                Products = data.Products.List(new QueryOptions<Product>
                {
                    OrderBy = c => c.Name
                }),
                Technicians = data.Technicians.List(new QueryOptions<Technician>
                {
                    OrderBy = c => c.Name
                })
                    
            };
            return model;
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            IncidentViewModel model = GetViewModel();
            model.Incident = new Incident();
            model.Action = "Add";
           // model passes instead of new Incident():
            return View("AddEdit", model);
        }
        

        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = GetViewModel();
            var incident = data.Incidents.Get(id);
            model.Incident = incident;
            model.Action = "Edit";
           
            return View("AddEdit", model);
        }
        [HttpGet]// delete from  db
        public IActionResult Delete(int id)
        {
            var incident = data.Incidents.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Incidents.Delete(incident);
            data.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    data.Incidents.Insert(incident);
                }
                else
                {
                    data.Incidents.Update(incident);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                IncidentViewModel model = GetViewModel();
                model.Incident = incident;
                if (incident.IncidentID == 0)
                {
                    model.Action = "Add";
                }
                else
                {
                    model.Action = "Edit";
                }
                return View("AddEdit", model);
            }
        }

    }
}
