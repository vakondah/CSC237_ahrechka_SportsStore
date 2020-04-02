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

        //This method returns ViewResult object
        // so ViewResult type was used
        [Route("products")]
        public ViewResult List()
        {
            ViewBag.Title = "Product List";
            List<Product> productList = context.Products.ToList();
            
            return View(productList);
        }


        // Product Manager screen 3 actions:

        //This method returns ViewResult object
        // so ViewResult type was used
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            
            return View("AddEdit", new Product());
        }
        //This method returns ViewResult object
        // so ViewResult type was used
        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View("AddEdit", product);
        }

        //This method returns RedirectToActionResult object
       // so RedirectToActionResult type  was used
       [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        // This method returns RedirectToActionResult
        // so RedirectToActionResult was used
        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            // TODO: TempData displays message without product name. Why?
            string message = product.Name + " was deleted.";
            context.Products.Remove(product);
            context.SaveChanges();
            TempData["message"] = message;
            return RedirectToAction("List");
        }

        // This method may return different types of result objects
        // so IActionResult type was used
        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message;
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
                    message = product.Name + " was added.";
                }
                else
                {
                    context.Products.Update(product);
                    message = product.Name + " was updated.";
                }
                context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", product);
            }

        }

    }

   
}
