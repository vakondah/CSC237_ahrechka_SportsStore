//Aliaksandra Hrechka
//CIS237
//04/21/2020
using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private IRepository<Customer> data{ get; set; }
        public CustomerController(IRepository<Customer> rep)
        {
            data = rep;
        }


        [Route("[controller]s")]
        public IActionResult List()
        {

            var customers = data.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            });
           
            return View(customers);
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            return View("AddEdit", new Customer());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            
            var customer = data.Get(id);
            return View("AddEdit", customer);
        }
        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var customer = data.Get(id);
            return View(customer);
        }
        // actual removing from db
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Save();
            data.Delete(customer);
            return RedirectToAction("List");
        }
        // save for add and edit
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.CountryID == "XX")
            {
                ModelState.AddModelError(nameof(Customer.CountryID), "Required");
            }

            if (customer.CustomerID == 0 || TempData["OkEmail"] == null)// only cheks new customer
            {
                string msg = Check.EmailExists(data, customer.Email);
                if (!String.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.Email), msg);
                }
            }

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    data.Insert(customer);
                }
                else
                {
                    data.Update(customer);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                if (customer.CustomerID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Avtion = "Edit";
                }
                return View("AddEdit", customer);
            }

        }
    }
}
