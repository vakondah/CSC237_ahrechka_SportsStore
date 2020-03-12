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
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;//DI
        }
        [Route("Products")]
        public IActionResult List()
        {
            ViewBag.Title = "Product List";

            var productList = _productRepository.GetProducts.ToList();
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
            var product = _productRepository.GetProductById(id);
            return View("AddEdit", product);
        }

        [HttpGet]// delete from  db?
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            return RedirectToAction("List");
        }
    }

   
}
