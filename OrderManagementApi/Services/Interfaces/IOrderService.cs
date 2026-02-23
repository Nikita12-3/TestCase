using OrderManagementApi.DTOs;

namespace OrderManagementApi.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetUserOrders(int userId, string sortBy);
        OrderDto GetOrderById(int orderId);
    }

}
