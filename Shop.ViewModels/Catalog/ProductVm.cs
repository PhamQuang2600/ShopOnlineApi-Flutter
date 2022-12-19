

namespace Shop.ViewModels.Catalog
{
    public class ProductVm
    {
        public int id { get; set; }
        public decimal? price { get; set; }
        public string createdDate { get; set; }
        public string name { set; get; }
        public string description { set; get; }
        public string category { set; get; }
        public string original { set; get; }
        public string imageProduct { set; get; }
    }
}
