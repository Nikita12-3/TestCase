using OrderManagementApi.DTOs;
using OrderManagementApi.Models;
using OrderManagementApi.Repositories.Interfaces;
using OrderManagementApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementApi.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderDto> GetUserOrders(int userId, string? sortBy)
        {
            var orders = _unitOfWork.Orders.GetUserOrders(userId);

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "price":
                        orders = orders.OrderBy(o => o.TotalPrice);
                        break;
                    case "priceDesc":
                        orders = orders.OrderByDescending(o => o.TotalPrice);
                        break;
                }
            }

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                ProductCount = o.OrderItems.Sum(oi => oi.Quantity),
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Product.Price
                }).ToList()
            });
        }

        public OrderDto GetOrderById(int orderId)
        {
            var order = _unitOfWork.Orders.GetOrderById(orderId);
            if (order == null)
                throw new KeyNotFoundException("Заказ не найден");

            return MapOrderToDto(order);
        }

        private OrderDto MapOrderToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                ProductCount = order.OrderItems.Sum(oi => oi.Quantity),
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Product.Price
                }).ToList()
            };
        }

    }
}
