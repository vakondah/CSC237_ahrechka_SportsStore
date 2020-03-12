using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class IncidentController: Controller
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITechnicianRepository _technicianRepository;

        public IncidentController(IIncidentRepository incidentRepository, ICustomerRepository customerRepository,
            IProductRepository productRepository, ITechnicianRepository technicianRepository)
        {
            _incidentRepository = incidentRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _technicianRepository = technicianRepository;
        }

        [Route("Incidents")]
        public IActionResult List(int productId)
        {
            ViewBag.Title = "Incident List";
            //ViewBag.Products = _productRepository.GetProducts.OrderBy(p => p.Name).ToList();
            //ViewBag.Product = _productRepository.GetProductById(productId);
            var incidentList = _incidentRepository.GetIncidents.ToList();
            return View(incidentList);
        }

        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Customers = _customerRepository.GetCustomers.OrderBy(c => c.FullName).ToList();
            ViewBag.Products = _productRepository.GetProducts.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = _technicianRepository.GetTechnicians.OrderBy(t => t.Name).ToList();
            return View("AddEdit", new Incident());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Customers = _customerRepository.GetCustomers.OrderBy(c => c.FullName).ToList();
            ViewBag.Products = _productRepository.GetProducts.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = _technicianRepository.GetTechnicians.OrderBy(t => t.Name).ToList();
            var incident = _incidentRepository.GetIncidentById(id);
            return View("AddEdit", incident);
        }
        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            return RedirectToAction("List");
        }
    }
}
