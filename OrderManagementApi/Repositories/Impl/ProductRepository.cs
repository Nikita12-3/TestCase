using OrderManagementApi.Data;
using OrderManagementApi.Models;
using OrderManagementApi.Repositories.Interfaces;

namespace OrderManagementApi.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderDbContext _context;

        public ProductRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}
