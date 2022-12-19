using Shop.ViewModels.Catalog;

namespace Shop.Application.Catalog
{
    public interface IProductsService
    {
        Task<ProductVm> GetByID(int productID);
        Task<List<ProductVm>> GetFeatureProduct( int take);
        Task<List<ProductVm>> GetLatestProduct(int take);
        Task<List<ProductVm>> SearchProduct(string keyword);
        Task<List<ProductVm>> GetSameProduct(int productID,int take);
    }
}
