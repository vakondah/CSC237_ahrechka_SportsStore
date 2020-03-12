using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class ProductController: Controller
    {
        private SportsProContext context;
        public ProductController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Products")]
        public IActionResult List()
        {
            ViewBag.Title = "Product List";
            List<Product> productList = context.Products.ToList();
            
            return View(productList);
        }


        // Product Manager screen 3 actions:
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View("AddEdit", product);
        }

        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (product.ProductID == 0)
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
                    context.Products.Add(product);
                }
                else
                {
                    context.Products.Update(product);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", product);
            }

        }

    }

   
}
