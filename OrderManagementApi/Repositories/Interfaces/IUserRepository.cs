using OrderManagementApi.Models;

namespace OrderManagementApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUsername(string username);
        void Add(User user);
    }

}
