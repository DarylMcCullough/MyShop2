using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> categories;

        // GET: ProductManager
        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            categories = new InMemoryRepository<ProductCategory>();
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.categories = categories.Collection();
            return View(viewModel);
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

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = product;
            viewModel.categories = categories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product product_existing = context.Find(id);
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