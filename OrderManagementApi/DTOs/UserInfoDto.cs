namespace OrderManagementApi.DTOs
{
    public class UserInfoDto
    {
        public UserDto User { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
