using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Catalog;

namespace ShopOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;
        public ProductsController(IProductsService productService)
        {

            _productService = productService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var product = await _productService.GetByID(id);
            return Ok(product);
        }
        [HttpGet("featured/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeatureProduct(int take)
        {
            var featureProducts = await _productService.GetFeatureProduct(take);
            return Ok(featureProducts);
        }
        [HttpGet("latested/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLastestProduct(int take)
        {
            var lastestProducts = await _productService.GetLatestProduct( take);
            return Ok(lastestProducts);
        }
        [HttpGet("same/{productId}/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSameProduct(int productId,int take)
        {
            var sameProducts = await _productService.GetSameProduct(productId, take);
            return Ok(sameProducts);
        }
        [HttpGet("search/{keyword}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProduct(string keyword)
        {
            var sameProducts = await _productService.SearchProduct(keyword);
            return Ok(sameProducts);
        }
    }
}
