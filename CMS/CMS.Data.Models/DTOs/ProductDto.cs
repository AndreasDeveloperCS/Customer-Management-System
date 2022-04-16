namespace CMS.Data.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PricePerUnit { get; set; }

        public string MeasuringUnit { get; set; }
    }
}
