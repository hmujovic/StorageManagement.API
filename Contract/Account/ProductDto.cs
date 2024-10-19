namespace Contract;

public class ProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public ICollection<ProductCategoryDto> ProductCategories { get; set; } = [];
}

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
}