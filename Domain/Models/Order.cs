namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string CustomerId { get; set; }
    public Account? Customer { get; set; } = null;

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}