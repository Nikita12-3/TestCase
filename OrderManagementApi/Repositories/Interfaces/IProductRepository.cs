using OrderManagementApi.Models;

namespace OrderManagementApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(int id);
        IEnumerable<Product> GetAll();
    }
}
