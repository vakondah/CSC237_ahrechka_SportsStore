//Aliaksandra Hrechka
//CIS237
//04/21/2020
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
        private IRepository<Incident> data { get; set; }
        public IncidentController(IRepository<Incident> rep)
        {
            data = rep;
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

            IEnumerable<Incident> incidents = data.List(options);
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

        [HttpGet]// display view without info
        public IActionResult Add()
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = new Incident(),
                Action = "Add"
            };
           // model passes instead of new Incident():
            return View("AddEdit", model);
        }
        

        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = data.Get(id),
                Action = "Edit"
            };
           
            return View("AddEdit", model);
        }
        [HttpGet]// delete from  db
        public IActionResult Delete(int id)
        {
            var incident = data.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Delete(incident);
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
                    data.Insert(incident);
                }
                else
                {
                    data.Update(incident);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                IncidentViewModel model = new IncidentViewModel
                {
                    Incident = incident
            };
                
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
