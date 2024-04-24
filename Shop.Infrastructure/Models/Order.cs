namespace Shop.Infrastructure.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }
}
