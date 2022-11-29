using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ViewModels.System
{
    public class UpdateCartRequest
    {
        public int ProductId { get; set; }
        public int? NumberProduct { get; set; }
        public decimal? FeeShipping { get; set; }
        public decimal? Total { get; set; }
    }
}
