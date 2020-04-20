using CSC237_ahrechka_SportsStore.DataLayer;
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
        private SportsProUnit data{ get; set; }
        public CustomerController(SportsProContext ctx)
        {
            data = new SportsProUnit(ctx);
        }


        [Route("customers")]
        public IActionResult List()
        {

            var customers = data.Customers.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            });
           
            return View(customers);
        }
        [HttpGet]// display view without info
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = GetCountryList();
            return View("AddEdit", new Customer());
        }
        [HttpGet]// Here we add info into view
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = GetCountryList();
            var customer = data.Customers.Get(id);
            return View("AddEdit", customer);
        }
        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var customer = data.Customers.Get(id);
            return View(customer);
        }
        // actual removing from db
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Save();
            data.Customers.Delete(customer);
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
                string msg = Check.EmailExists(data.Customers, customer.Email);
                if (!String.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.Email), msg);
                }
            }

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    data.Customers.Insert(customer);
                }
                else
                {
                    data.Customers.Update(customer);
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

        // private helper method
        IEnumerable<Country> GetCountryList() =>
            data.Countries.List(new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            });
    }
}
