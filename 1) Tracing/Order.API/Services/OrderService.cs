using Azure.Core;
using Common.Shared.DTOs;
using Order.API.Models;
using Order.API.RedisServices;
using Order.API.StockServices;
using Shared;
using System.Diagnostics;

namespace Order.API.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;
        private readonly StockService _stockService;
        private readonly RedisService _redisService;

        public OrderService(AppDbContext context, StockService stockService,RedisService redisService)
        {
            _context = context;
            _stockService = stockService;
            _redisService = redisService;
        }

        public async Task<OrderCreateResponseDto> CreateAsync(OrderCreateRequestDto requestDto)
        {


            using (var redisActivity = ActivitySourceProvider.Source.StartActivity("RedisStringSetGet"))
            {
                // redis için örnek kod
                await _redisService.GetDb(0).StringSetAsync("userId", requestDto.UserId);

                redisActivity.SetTag("userId", requestDto.UserId);

                var redisUserId = _redisService.GetDb(0).StringGetAsync("UserId");
            }



            Activity.Current?.SetTag("Asp.Net Core(instrumentation) tag1", "Asp.Net Core(instrumentation) tag value");
            using var activity = ActivitySourceProvider.Source.StartActivity();
            activity?.AddEvent(new("Sipariş süreci başladı."));


            var newOrder = new Models.Order()
            {
                Created = DateTime.Now,
                OrderCode = Guid.NewGuid().ToString(),
                Status = OrderStatus.Success,
                UserId = requestDto.UserId,
                Items = requestDto.Items.Select(x => new OrderItem()
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice
                }).ToList()


            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            StockCheckAndPaymentProcessRequestDto stockRequest = new();

            stockRequest.OrderCode = newOrder.OrderCode;
            stockRequest.OrderItems = requestDto.Items;

            var stockResponse = await _stockService.CheckStockAndPaymentStartAsync(stockRequest);


            activity.SetTag("order user id", requestDto.UserId);

            activity?.AddEvent(new("Sipariş süreci tamamlandı."));

            return new OrderCreateResponseDto() { Id = newOrder.Id };

        }
    }
}