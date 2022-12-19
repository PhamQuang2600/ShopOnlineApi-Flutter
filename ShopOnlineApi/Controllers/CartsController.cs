using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.System;
using Shop.ViewModels.System;

namespace ShopOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{uid:Guid}")]
        public async Task<IActionResult> GetAll(Guid uid)
        {
            var carts = await _cartService.GetAll(uid);
            if(carts == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(carts);
        }
        [HttpGet("{cartId:int}")]
        public async Task<IActionResult> GetById(int cartId)
        {
            var carts = await _cartService.GetById(cartId);
            if(carts == null)
            {
              return BadRequest();
            }
            return Ok(carts);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] AddCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _cartService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }

            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update( [FromForm] UpdateCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _cartService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> Delete(int cartId)
        {
            var affectedResult = await _cartService.Delete(cartId);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
