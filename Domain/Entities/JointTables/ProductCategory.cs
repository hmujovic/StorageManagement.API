﻿namespace Domain.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }

    public Product Product { get; set; } = new();
    public Category Category { get; set; } = new();
}