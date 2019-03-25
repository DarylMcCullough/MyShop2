using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public  class Product : BaseEntity
    {
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { set; get; }
        public string Description { set; get;  }
        public string Category { set; get; }
        public decimal Price { set; get; }
        public string Image { set; get; }
    }

    
}
