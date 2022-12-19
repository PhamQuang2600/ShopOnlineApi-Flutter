using Shop.Data.Enum;
using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
            public decimal? ProductPrice { get; set; }
        public int? NumberProduct { get; set; }
        public decimal? FeeShipping { get; set; }
        public DateTime? DateAddCart { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalAll { get; set; }
        public int? CounterInCart { get; set; }
         public EnumType.Payment StatusPayment { get; set; }

        public User Users { get; set; }
        public Product Products { get; set; }
    }
}
