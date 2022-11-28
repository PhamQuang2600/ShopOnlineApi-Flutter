

namespace Shop.ViewModels.Catalog
{
    public class ProductVm
    {
        public int ID { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string TypeProduct { set; get; }
        public string Original { set; get; }
        public string ImageProduct { set; get; }
    }
}
