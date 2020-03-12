using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICountryRepository _countryRepository;

        public CustomerController(ICustomerRepository customerRepository, ICountryRepository countryRepository)
        {
            _customerRepository = customerRepository;
            _countryRepository = countryRepository;
        }
        [Route("Customers")]
        public IActionResult List()
        {
            ViewBag.Title = "Customer List";
            var customerList = _customerRepository.GetCustomers.ToList();
            return View(customerList);
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = _countryRepository.Countries.OrderBy(c => c.Name).ToList();
            return View("AddEdit", new Customer());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = _countryRepository.Countries.OrderBy(c => c.Name).ToList();
            var customer = _customerRepository.GetCustomerById(id);
            return View("AddEdit", customer);
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
