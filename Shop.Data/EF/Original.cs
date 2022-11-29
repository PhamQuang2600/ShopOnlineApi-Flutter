using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EF
{
    public class Original
    {
        public int Id { get; set; }
       public string Name { get; set; }
        public virtual ICollection<OriginalProduct> OriginalProducts { get; set; }

    }
}
