using Microsoft.AspNetCore.Mvc;
using shop.Dtos;
using shop.Services;

namespace shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST /api/orders/from-cart
        [HttpPost("from-cart")]
        public async Task<ActionResult<OrderResponse>> CreateOrderFromCart(CreateOrderFromCartRequest request)
        {
            try
            {
                var order = await _orderService.CreateOrderFromCartAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        // GET /api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}


