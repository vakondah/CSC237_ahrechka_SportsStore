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
        private SportsProContext context;
        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("incidents")]
        public IActionResult List(string filter = "all")
        {
            // To use view model create new instance of IncidentListViewModel:
            IncidentListViewModel model = new IncidentListViewModel()
            {
                Filter = filter
            };
            ViewBag.Title = "Incident List";
            IQueryable<Incident> query = context.Incidents
                 .Include(i => i.Customer)
                 .Include(i => i.Product)
                 .OrderBy(i => i.DateOpened);

            if (filter == "unassigned")
            {
                query = query.Where(i => i.TechnicianID == null);
            }
            if (filter == "open")
            {
                query = query.Where(i => i.DateClosed == null);
            }

            List<Incident> incidents = query.ToList();
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
                Customers = context.Customers
                    .OrderBy(c => c.FirstName)
                    .ToList(),
                Products = context.Products
                    .OrderBy(p => p.Name)
                    .ToList(),
                 Technicians = context.Technicians
                    .OrderBy(t => t.Name)
                    .ToList()
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
            var incident = context.Incidents.Find(id);
            model.Incident = incident;
            model.Action = "Edit";
           
            return View("AddEdit", model);
        }
        [HttpGet]// delete from  db
        public IActionResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (incident.IncidentID == 0)
            {
                ViewBag.Action = "Add";
            }
            else
            {
                ViewBag.Action = "Edit";
            }

            if (ModelState.IsValid)
            {
                if (ViewBag.Action == "Add")
                {
                    context.Incidents.Add(incident);
                }
                else
                {
                    context.Incidents.Update(incident);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", incident);
            }
        }

    }
}
