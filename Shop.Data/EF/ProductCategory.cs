using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EF
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public Product Products { get; set; }
        public Category Categories { get;}
    }
}
