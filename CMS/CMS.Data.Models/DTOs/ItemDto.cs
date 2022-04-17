namespace CMS.Data.Models.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public int OrderId { get; set; }
        public OrderDto Order { get; set; }

        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }
}
