﻿namespace Order.API.Services
{
    public record OrderCreateRequestDto
    {
        public int UserId { get; set; }
        public List<Common.Shared.DTOs.OrderItemDto> Items { get; set; } = null!;
    }

    public record OrderItemDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
    }
}