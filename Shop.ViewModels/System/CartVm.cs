using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ViewModels.System
{
    public class CartVm
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int? NumberProduct { get; set; }
        public decimal? FeeShipping { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalAll { get; set; }
        public int? CounterInCart { get; set; }
    }
}
