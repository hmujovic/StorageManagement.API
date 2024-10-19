using Domain.Entities;

namespace Domain.Repositories;

public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
{
    Task<IEnumerable<ProductCategory>> GetAll(CancellationToken cancellationToken = default);

    Task<IEnumerable<ProductCategory>> GetByProductId(int productId, CancellationToken cancellationToken = default);

    Task<ProductCategory> GetById(int productCategoryId, CancellationToken cancellationToken = default);

    void CreateProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default);

    void DeleteProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default);

    void UpdateProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default);
}