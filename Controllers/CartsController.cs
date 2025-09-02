using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using shop.Dtos;
using shop.Services;

namespace shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

      
        [HttpPost]
        public async Task<ActionResult<CartResponse>> CreateCart(CreateCartRequest request)
        {
            var cart = await _cartService.CreateCartAsync(request);
            return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, cart);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<CartResponse>> GetCartById(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

       
        [HttpPost("{cartId}/items")]
        public async Task<ActionResult<CartResponse>> AddCartItem(int cartId, AddCartItemRequest request)
        {
            try
            {
                var cart = await _cartService.AddCartItemAsync(cartId, request);
                return Ok(cart);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Conflict(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        
        [HttpGet("{cartId}/items")]
        public async Task<ActionResult<IEnumerable<CartItemResponse>>> GetCartItems(int cartId)
        {
            try
            {
                var items = await _cartService.GetCartItemsAsync(cartId);
                return Ok(items);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}
