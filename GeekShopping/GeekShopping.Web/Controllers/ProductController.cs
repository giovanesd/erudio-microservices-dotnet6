using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        // GET: /<controller>/
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProdcuts();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(productModel);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.UpdateProduct(productModel);
                if (product == null) return View(productModel);
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductEdit(long id)
        {
            var product = await _productService.FindProductById(id);
            return View(product);
        }

        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.FindProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.DeleteProductById(productModel.Id);
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(productModel);
        }
    }
}

