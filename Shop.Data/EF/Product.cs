using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? TypeProduct { get; set; }
        public string? Original { get; set; }
        public string? ImageProduct { get; set; }
    }
}
