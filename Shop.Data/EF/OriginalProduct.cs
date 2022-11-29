using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EF
{
    public class OriginalProduct
    {
        public int OriginalId { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set;}
        public Original Originals { get; set;}

    }
}
