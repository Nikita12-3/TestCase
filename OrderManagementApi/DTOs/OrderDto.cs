namespace OrderManagementApi.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductCount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
