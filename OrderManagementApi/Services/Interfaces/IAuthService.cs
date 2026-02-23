namespace OrderManagementApi.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
    }
}
