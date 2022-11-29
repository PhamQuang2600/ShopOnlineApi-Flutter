using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Catalog;

namespace ShopOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalsController : ControllerBase
    {
        private readonly IOriginalService _originalService;
        public OriginalsController(IOriginalService originalService)
        {
            _originalService = originalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _originalService.GetAll();
            return Ok(categories);
        }
    }
}
