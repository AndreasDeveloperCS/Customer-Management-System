namespace CMS.Data.Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public CustomerDto Customer { get; set; }
        public int CustomerId { get; set; }

        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
