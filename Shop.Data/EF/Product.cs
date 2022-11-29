using Shop.Data.Enum;
using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public int Stock { get; set; }  
        public string? ImageProduct { get; set; }

        public virtual ICollection<Cart> Carts { get; set;}
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<OriginalProduct> OriginalProducts { get; set; }
    }
}
