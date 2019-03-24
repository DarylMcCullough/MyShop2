using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        // GET: ProductManager
        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (! ModelState.IsValid)
            {
                return View(product);
            }
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product product_existing = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (! ModelState.IsValid)
            {
                return View(product);
            }
            product_existing.Category = product.Category;
            product_existing.Description = product.Description;
            product_existing.Image = product.Image;
            product_existing.Price = product.Price;
            product_existing.Name = product.Name;

            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            context.Delete(id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}