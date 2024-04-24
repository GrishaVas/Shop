namespace Shop.Services.Models
{
    public class OrderDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
