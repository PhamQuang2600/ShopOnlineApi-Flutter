using Shop.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EF
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public EnumType.Status Status { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
