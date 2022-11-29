using Shop.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Catalog
{
    public interface IProductsService
    {
        Task<ProductVm> GetByID(int productID);
        Task<List<ProductVm>> GetFeatureProduct( int take);
        Task<List<ProductVm>> GetLatestProduct(int take);
        Task<List<ProductVm>> GetSameProduct(int productID,int take);
    }
}
