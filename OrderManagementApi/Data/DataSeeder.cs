using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementApi.Models;
using System;
using System.Linq;

namespace OrderManagementApi.Data
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new OrderDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<OrderDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = UserRole.Admin },
                        new User { Username = "user1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = UserRole.User },
                        new User { Username = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = UserRole.User }
                    );
                    context.SaveChanges();
                }
                if (context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Name = "Product 1", Price = 100 },
                        new Product { Name = "Product 2", Price = 200 },
                        new Product { Name = "Product 3", Price = 300 },
                        new Product { Name = "Product 4", Price = 400 },
                        new Product { Name = "Product 5", Price = 500 }
                    );
                    context.SaveChanges();
                }
                if (context.Orders.Any())
                {
                    var admin = context.Users.First(u => u.Username == "admin");
                    var user1 = context.Users.First(u => u.Username == "user1");
                    var user2 = context.Users.First(u => u.Username == "user2");
                    var products = context.Products.ToList();
                    var adminOrder1 = new Order
                    {
                        UserId = admin.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        TotalPrice = products[0].Price * 1 + products[1].Price * 2,
                        OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = products[0].Id, Quantity = 1, Price = products[0].Price },
                    new OrderItem { ProductId = products[1].Id, Quantity = 2, Price = products[1].Price }
                }
                    };
                    var adminOrder2 = new Order
                    {
                        UserId = admin.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-10),
                        TotalPrice = products[2].Price * 1 + products[3].Price * 1,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem { ProductId = products[2].Id, Quantity = 1, Price = products[2].Price },
                            new OrderItem { ProductId = products[3].Id, Quantity = 1, Price = products[3].Price }
                        }
                    };
                    var user1Order1 = new Order
                    {
                        UserId = user1.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-3),
                        TotalPrice = products[0].Price * 2 + products[2].Price * 1,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem { ProductId = products[0].Id, Quantity = 2, Price = products[0].Price },
                            new OrderItem { ProductId = products[2].Id, Quantity = 1, Price = products[2].Price }
                        }
                    };
                    var user1Order2 = new Order
                    {
                        UserId = user1.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-7),
                        TotalPrice = products[1].Price * 1 + products[4].Price * 1,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem { ProductId = products[1].Id, Quantity = 1, Price = products[1].Price },
                            new OrderItem { ProductId = products[4].Id, Quantity = 1, Price = products[4].Price }
                        }
                    };
                    var user2Order1 = new Order
                    {
                        UserId = user2.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-2),
                        TotalPrice = products[1].Price * 3 + products[3].Price * 2,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem { ProductId = products[1].Id, Quantity = 3, Price = products[1].Price },
                            new OrderItem { ProductId = products[3].Id, Quantity = 2, Price = products[3].Price }
                        }
                    };

                    var user2Order2 = new Order
                    {
                        UserId = user2.Id,
                        OrderDate = DateTime.UtcNow.AddDays(-4),
                        TotalPrice = products[0].Price * 1 + products[4].Price * 2,
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem { ProductId = products[0].Id, Quantity = 1, Price = products[0].Price },
                            new OrderItem { ProductId = products[4].Id, Quantity = 2, Price = products[4].Price }
                        }
                    };

                    context.Orders.AddRange(adminOrder1, adminOrder2, user1Order1, user1Order2, user2Order1, user2Order2);
                    context.SaveChanges();
                }
            }
        }

    }
}
