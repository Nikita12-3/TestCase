namespace OrderManagementApi.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        void Commit();
    }
}
