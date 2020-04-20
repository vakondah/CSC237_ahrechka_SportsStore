using CSC237_ahrechka_SportsStore.DataLayer;
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
        private Repository<Product> data { get; set; }
        public ProductController(SportsProContext ctx)
        {
            data = new Repository<Product>(ctx);
        }

        //This method returns ViewResult object
        // so ViewResult type was used
        [Route("products")]
        public ViewResult List()
        {
            ViewBag.Title = "Product List";

            var products = this.data.List(new QueryOptions<Product>
            {
                OrderBy = p => p.ReleaseDate
            });

            return View(products);
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
            var product = data.Get(id);
            return View("AddEdit", product);
        }

        //This method returns RedirectToActionResult object
       // so RedirectToActionResult type  was used
       [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = data.Get(id);
            return View(product);
        }

        // This method returns RedirectToActionResult
        // so RedirectToActionResult was used
        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            // TODO: TempData displays message without product name. Why?
            string message = product.Name + " was deleted.";
            data.Delete(product);
            data.Save();
            TempData["message"] = message;
            return RedirectToAction("List");
        }

        // This method may return different types of result objects
        // so IActionResult type was used
        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message;
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    data.Insert(product);
                    message = product.Name + " was added.";
                }
                else
                {
                    data.Update(product);
                    message = product.Name + " was updated.";
                }
                data.Save();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (product.ProductID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(product);
            }

        }

    }

   
}
