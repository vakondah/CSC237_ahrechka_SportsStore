using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using CSC237_ahrechka_SportsStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProUnit data { get; set; }
        public RegistrationController(SportsProContext ctx) => data = new SportsProUnit(ctx);

        public IActionResult GetCustomer()
        {
            ViewBag.Customers = data.Customers.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            });
                
            int custID = HttpContext.Session.GetInt32("custID") ?? 0;

            Customer customer;
            if (custID == 0)
            {
                customer = new Customer();
            }
            else
            {
                customer = data.Customers.Get(custID);
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult List(Customer customer)
        {
            HttpContext.Session.SetInt32("custID", customer.CustomerID);

            if (customer.CustomerID == 0)
            {
                TempData["message"] = "You must select a customer.";
                return RedirectToAction("GetCustomer");
            }
            else
            {
                return RedirectToAction("List", new { id = customer.CustomerID });
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            RegistrationViewModel model = new RegistrationViewModel
            {
                CustomerID = id,
                Customer = data.Customers.Get(id),
                Products = data.Products.List(new QueryOptions<Product>
                {
                    OrderBy = p => p.Name
                }),
                
                Registrations = data.Registrations.List(new QueryOptions<Registration>
                {
                    Includes = "Customer, Product",
                    Where = r => r.CustomerID == id
                })
                
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int customerID, int productID)
        {
            Registration registration = new Registration
            {
                CustomerID = customerID,
                ProductID = productID
            };
            data.Registrations.Delete(registration);
            data.Save();
            return RedirectToAction("List", new { ID = customerID });
        }

        [HttpPost]
        public IActionResult Filter(int customerID = 0)
        {
            return RedirectToAction("List", new { ID = customerID });
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (model.ProductID == 0)
            {
                TempData["message"] = "You must select a product.";
            }
            else
            {
                Registration registration = new Registration
                {
                    CustomerID = model.CustomerID,
                    ProductID = model.ProductID
                };
                data.Registrations.Insert(registration);
                try
                {
                    data.Save();
                }
                catch (DbUpdateException ex)
                {

                    string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                    if (msg.Contains("duplicate key"))
                    {
                        TempData["message"] = "This product is already registered to this customer.";
                    }
                    else
                    {
                        TempData["message"] = "Error accessing the database.";
                    }
                }
            }
            return RedirectToAction("List", new { ID = model.CustomerID });
        }

    }
}
