using CSC237_ahrechka_SportsStore.Models;
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
        public IActionResult List()
        {
            ViewBag.Title = "Incident List";
            List <Incident> incidents = context.Incidents
                 .Include(i => i.Customer)
                 .Include(i => i.Product)
                 .OrderBy(i => i.DateOpened)
                 .ToList();
            
            return View(incidents);
        }
        public void StoreListsInViewBag()
        {
            //ViewBag.Action = "Edit";
            ViewBag.Customers = context.Customers
                .OrderBy(c => c.FirstName)
                .ToList();
            ViewBag.Products = context.Products
                .OrderBy(p => p.Name)
                .ToList();
            ViewBag.Technicians = context.Technicians
                .OrderBy(t => t.Name)
                .ToList();
        }

        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            StoreListsInViewBag();
            return View("AddEdit", new Incident());
        }
        

        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            StoreListsInViewBag();
            var incident = context.Incidents.Find(id);
            return View("AddEdit", incident);
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
        public IActionResult Save(Incident i)
        {
            if (i.IncidentID == 0)
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
                    context.Incidents.Add(i);
                }
                else
                {
                    context.Incidents.Update(i);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", i);
            }

        }

    }
}
