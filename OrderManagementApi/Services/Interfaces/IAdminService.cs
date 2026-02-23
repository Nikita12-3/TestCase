using OrderManagementApi.DTOs;

namespace OrderManagementApi.Services.Interfaces
{
    public interface IAdminService
    {
        UserInfoDto GetUserInfo(int userId);
        IEnumerable<OrderDto> FilterOrders(decimal? minPrice, decimal? maxPrice, DateTime? startDate, string productName);
    }
}
