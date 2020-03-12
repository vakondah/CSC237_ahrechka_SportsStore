using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class TechnicianController: Controller
    {
        private SportsProContext context;

        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            ViewBag.Title = "Technician List";
            List<Technician> techs = context.Technicians.ToList();
            return View(techs);
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var tech = context.Technicians.Find(id);
            return View("AddEdit", tech);
        }
        [HttpGet]// delete action
        public IActionResult Delete(int id)
        {
            var tech = context.Technicians.Find(id);
            return View(tech);
        }

        [HttpPost]
        public IActionResult Delete(Technician tech)
        {
            context.Technicians.Remove(tech);
            context.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Save(Technician tech)
        {
            if (tech.TechnicianID == 0)
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
                    context.Technicians.Add(tech);
                }
                else
                {
                    context.Technicians.Update(tech);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", tech);
            }

        }
    }
}
