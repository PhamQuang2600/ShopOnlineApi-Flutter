using System;
using System.Collections.Generic;

namespace Shop.Data.EF
{
    public partial class Cart
    {
        public string Uid { get; set; } = null!;
        public int ProductId { get; set; }
        public int? NumberProduct { get; set; }
        public double? FeeShipping { get; set; }
        public double? Total { get; set; }
        public int? CounterInCart { get; set; }
    }
}
