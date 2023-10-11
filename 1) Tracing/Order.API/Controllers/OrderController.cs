using Common.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.Services;

namespace Order.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(OrderService orderService, IPublishEndpoint publishEndpoint)
        {
            _orderService = orderService;
            _publishEndpoint = publishEndpoint;
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

        [HttpGet]
        public async Task<IActionResult> SendOrderCreatedEvent()
        {

            // Kuyruğua mesaj gönder

            await _publishEndpoint.Publish(new OrderCreatedEvent() { OrderCode = Guid.NewGuid().ToString() });

            return Ok();

        }
    }
}
