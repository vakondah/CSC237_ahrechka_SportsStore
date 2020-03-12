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
        private readonly ITechnicianRepository _technicianRepository;

        public TechnicianController(ITechnicianRepository technicianRepository)
        {
            _technicianRepository = technicianRepository;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            ViewBag.Title = "Technician List";
            var technicianList = _technicianRepository.GetTechnicians.ToList();
            return View(technicianList);
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
            var technician = _technicianRepository.GetTechnicianById(id);
            return View("AddEdit", technician);
        }
        [HttpGet]// delete action
        public IActionResult Delete(int id)
        {
            var technician = _technicianRepository.GetTechnicianById(id);
            return View(technician);
        }

        [HttpPost]//?
        public IActionResult Delete(Technician technician)
        {
            return RedirectToAction("List");
        }
    }
}
