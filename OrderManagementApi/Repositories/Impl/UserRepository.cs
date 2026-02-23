using OrderManagementApi.Data;
using OrderManagementApi.Models;
using OrderManagementApi.Repositories.Interfaces;

namespace OrderManagementApi.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderDbContext _context;

        public UserRepository(OrderDbContext context)
        {
            _context = context;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }

}
