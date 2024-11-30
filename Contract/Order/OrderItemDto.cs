namespace Contract;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemCreateDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemUpdateDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}