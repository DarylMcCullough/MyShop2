using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        // GET: ProductCategoryManager
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }

        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductCategory product = new ProductCategory();
            return View(product);
        }
        
        [HttpPost]
        public ActionResult Create(ProductCategory product)
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
            ProductCategory product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory product, string id)
        {
            ProductCategory product_existing = context.Find(id);
            if (product_existing == null)
            {
                return HttpNotFound();
            }
            if (! ModelState.IsValid)
            {
                return View(product);
            }
            product_existing.Category = product.Category;
            product_existing.Description = product.Description;

            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            ProductCategory product = context.Find(id);
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
            ProductCategory product = context.Find(id);
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