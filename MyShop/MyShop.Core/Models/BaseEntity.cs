using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public abstract class BaseEntity
    {
        public string ID { set; get; }
        public DateTimeOffset created;

        public BaseEntity()
        {
            ID = Guid.NewGuid().ToString();
            created = DateTime.Now;
        }
    }
}
