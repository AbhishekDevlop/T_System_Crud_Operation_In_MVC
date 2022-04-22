using Microsoft.AspNetCore.Mvc;
using CRUD_Operation_In_MVC.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace CRUD_Operation_In_MVC.Controllers
{
    public class ProductsController : Controller
    {
        ProductDAL context = new ProductDAL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List() 
        {
            ViewBag.ProductList = context.GetAllProducts();
            return View();
        }

        public IActionResult Create() 
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            Products p = new Products();
            p.Name = form["name"];
            p.Price = Convert.ToDecimal(form["price"]);
            int res = context.Save(p);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Products prod = context.GetProductById(id);
            ViewBag.Name = prod.Name;
            ViewBag.Price = prod.Price;
            ViewBag.Id = prod.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Products prod = new Products();
            prod.Name = form["name"];
            prod.Price = Convert.ToDecimal(form["price"]);
            prod.Id = Convert.ToInt32(form["id"]);
            int res = context.Upate(prod);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Products prod = context.GetProductById(id);
            ViewBag.Name = prod.Name;
            ViewBag.Price = prod.Price;
            ViewBag.Id = prod.Id;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int res = context.Delete(id);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }



    }
}
