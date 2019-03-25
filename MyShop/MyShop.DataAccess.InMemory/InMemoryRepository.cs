using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache objectCache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = objectCache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            objectCache[className] = items;
        }

        public void Insert(T item)
        {
            items.Add(item);
        }

        public void Delete(string id)
        {
            T old_item = items.Find(item1 => item1.ID == id);
            if (old_item == null)
            {
                throw new Exception(className + "not found");
            }
            items.Remove(old_item);
        }

        public void Update(T item)
        {
            Delete(item.ID);
            items.Add(item);
        }

        public T Find(string id)
        {
            T item = items.Find(p => p.ID == id);
            if (item == null)
            {
                throw new Exception(className + "not found");
            }
            return item;
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable<T>();
        }
    }
}
