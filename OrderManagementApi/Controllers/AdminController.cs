using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Services.Interfaces;

namespace OrderManagementApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            try
            {
                var userInfo = _adminService.GetUserInfo(userId);
                return Ok(userInfo);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("orders/filter")]
        public IActionResult FilterOrders(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] DateTime? startDate,
            [FromQuery] string? productName)
        {
            var filteredOrders = _adminService.FilterOrders(minPrice, maxPrice, startDate, productName);
            return Ok(filteredOrders);
        }
    }
}
