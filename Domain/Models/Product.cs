namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }

    public Category? Category { get; set; } = null;
    public ICollection<OrderItem>? OrderItems { get; set; } = [];
}