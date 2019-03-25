using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache objectCache = MemoryCache.Default;
        List<ProductCategory> categories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            categories = objectCache["categories"] as List<ProductCategory>;
            if (categories == null)
            {
                categories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            objectCache["categories"] = categories;
        }

        public void Insert(ProductCategory cat)
        {
            categories.Add(cat);
        }

        public void Update(ProductCategory cat)
        {
            ProductCategory cat_existing = categories.Find(cat1 => cat1.ID == cat.ID);
            if (cat_existing == null)
            {
                throw new Exception("Category not found");
            }
            cat_existing = cat;
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory category = categories.Find(cat => cat.ID == Id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public IQueryable<ProductCategory> Collection()
        {
            return categories.AsQueryable<ProductCategory>();
        }

        public void Delete(String id)
        {
            ProductCategory category = categories.Find(cat => cat.ID == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            categories.Remove(category);

        }
    }
}
