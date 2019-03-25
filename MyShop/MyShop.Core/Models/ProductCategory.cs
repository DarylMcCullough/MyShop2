using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string ID { set; get; }
        public string Category { set; get; }
        public string Description { set; get; }
        public ProductCategory()
        {
            ID = Guid.NewGuid().ToString();
        }
    }


}