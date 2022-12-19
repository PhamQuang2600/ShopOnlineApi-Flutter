using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ViewModels.System
{
    public class AddCartRequest
    {
        public Guid Uid { get; set; }
        public int ProductId { get; set; }
        
    }
}
