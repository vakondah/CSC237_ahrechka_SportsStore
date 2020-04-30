﻿//Aliaksandra Hrechka
//CIS237
//04/21/2020
using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CSC237_ahrechka_SportsStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TechnicianController: Controller
    {
        private IRepository<Technician> data { get; set; }

        public TechnicianController(IRepository<Technician> rep)
        {
            data = rep;
        }

        [Route("technicians")]
        public IActionResult List()
        {
            ViewBag.Title = "Technician List";

            var technicians = this.data.List(new QueryOptions<Technician>
            {
                OrderBy = t => t.Name
            });

            return View(technicians);
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
            var tech = data.Get(id);
            return View("AddEdit", tech);
        }
        [HttpGet]// delete action
        public IActionResult Delete(int id)
        {
            var tech = data.Get(id);
            return View(tech);
        }

        [HttpPost]
        public IActionResult Delete(Technician tech)
        {
            data.Delete(tech);
            data.Save();
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Save(Technician tech)
        {
            if (ModelState.IsValid)
            {
                if (tech.TechnicianID == 0)
                {
                    data.Insert(tech);
                }
                else
                {
                    data.Update(tech);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                if (tech.TechnicianID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(tech);
            }
        }
    }
}
