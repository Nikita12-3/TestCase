using OrderManagementApi.DTOs;
using OrderManagementApi.Repositories.Interfaces;
using OrderManagementApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementApi.Services.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserInfoDto GetUserInfo(int userId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            if (user == null)
                throw new KeyNotFoundException("Пользователь не найден");

            var userOrders = _unitOfWork.Orders.GetUserOrders(userId);
            var totalOrders = userOrders.Count();
            var totalPrice = userOrders.Sum(o => o.TotalPrice);

            return new UserInfoDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role.ToString()
                },
                TotalOrders = totalOrders,
                TotalPrice = totalPrice
            };
        }

        public IEnumerable<OrderDto> FilterOrders(decimal? minPrice, decimal? maxPrice, DateTime? startDate, string productName)
        {
            var query = _unitOfWork.Orders.GetAll().AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(o => o.TotalPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(o => o.TotalPrice <= maxPrice.Value);

            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate.Value);

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(o => o.OrderItems.Any(oi => oi.Product.Name.Contains(productName)));

            var filteredOrders = query.ToList();
            return filteredOrders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
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
    }
}
