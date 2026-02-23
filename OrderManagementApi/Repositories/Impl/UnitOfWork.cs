using OrderManagementApi.Data;
using OrderManagementApi.Repositories.Interfaces;

namespace OrderManagementApi.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
