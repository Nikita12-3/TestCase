using OrderManagementApi.Models;

namespace OrderManagementApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetUserOrders(int userId);
        Order GetOrderById(int orderId);
        void Add(Order order);
        IEnumerable<Order> GetAll();
    }

}
