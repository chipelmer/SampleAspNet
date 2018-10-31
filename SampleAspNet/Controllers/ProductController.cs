using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleAspNet.ViewModels;
using SampleAspNet.Models;

namespace SampleAspNet.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ProductRepository repo = new ProductRepository();

            ProductIndexViewModel viewModel = new ProductIndexViewModel();
            viewModel.Products = repo.GetAllProducts();

            return View(viewModel);
        }

        public IActionResult Delete(int ProductIdToDelete)
        {
            ProductRepository repo = new ProductRepository();
            repo.DeleteProduct(ProductIdToDelete);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult NewProduct()
        {
            return View();
        }

        public IActionResult Add(string name, decimal price)
        {
            ProductRepository repo = new ProductRepository();
            repo.InsertProduct(name, price, 1);

            return RedirectToAction("Index", "Product");
        }
    }
}