﻿namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = [];
}