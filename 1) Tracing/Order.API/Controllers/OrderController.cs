using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Services;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<IActionResult> Get(OrderCreateRequestDto request)
        {
            var result = await _orderService.CreateAsync(request);
            //var a = 10;
            //var b = 0;

            //var c= a / b;

            return Ok(result);
        }
    }
}
