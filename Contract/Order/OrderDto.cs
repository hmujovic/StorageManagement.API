namespace Contract;

public class OrderDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string? CustomerId { get; set; }
    public AccountDto? Customer { get; set; } = null;
}

public class OrderCreateDto
{
    public decimal TotalPrice { get; set; }
    public string? CustomerId { get; set; }
}

public class OrderUpdateDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
}