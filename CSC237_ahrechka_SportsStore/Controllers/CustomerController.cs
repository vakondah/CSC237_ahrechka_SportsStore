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
        private SportsProContext context { get; set; }
        public CustomerController(SportsProContext ctx)
        {
            context = ctx; 
        }


        [Route("Customers")]
        public IActionResult List()
        {
            
            List<Customer> customers = context.Customers
                .OrderBy(c => c.LastName).ToList();
            //var customerList = _customerRepository.GetCustomers.ToList();
            return View(customers);
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.ToList();
            return View("AddEdit", new Customer());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.ToList();
            var customer = context.Customers.Find(id);
            return View("AddEdit", customer);
        }
        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }
        // actual removing from db
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("List");
        }
        // save for add and edit
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.CustomerID == 0)
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
                    context.Customers.Add(customer);
                }
                else
                {
                    context.Customers.Update(customer);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Countries = context.Countries.ToList();
                return View("AddEdit", customer);
            }

        }
    }
}
