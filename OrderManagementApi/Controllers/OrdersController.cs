using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Models;
using OrderManagementApi.Services.Interfaces;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserOrders(int userId, [FromQuery] string? sortBy = null)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole == UserRole.Admin.ToString() || currentUserId == userId)
        {
            var orders = _orderService.GetUserOrders(userId, sortBy);
            return Ok(orders);
        }

        return Forbid();
    }



    [HttpGet("{orderId}")]
    public IActionResult GetOrder(int orderId)
    {
        var order = _orderService.GetOrderById(orderId);

        if (order == null)
            return NotFound();

        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (currentUserIdClaim == null)
            return Unauthorized("User is not authenticated.");

        var currentUserId = int.Parse(currentUserIdClaim.Value);
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole == UserRole.Admin.ToString() || order.UserId == currentUserId)
        {
            return Ok(order);
        }

        return Forbid();
    }

}
