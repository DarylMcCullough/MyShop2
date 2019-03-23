using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache objectCache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = objectCache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            objectCache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product p_existing = products.Find(p1 => p1.ID == p.ID);
            if (p_existing == null)
            {
                throw new Exception("Product not found");
            }
            p_existing = p;
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.ID == Id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable<Product>();
        }

        public void Delete(String id)
        {
            Product product = products.Find(p => p.ID == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            products.Remove(product);

        }
    }
}
