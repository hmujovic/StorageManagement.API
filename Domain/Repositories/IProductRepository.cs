using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetByCategoryId(int categoryId, CancellationToken cancellationToken = default);

    Task<Product> GetById(int productId, CancellationToken cancellationToken = default);

    void CreateProduct(Product product, CancellationToken cancellationToken = default);

    void DeleteProduct(Product product, CancellationToken cancellationToken = default);

    void UpdateProduct(Product product, CancellationToken cancellationToken = default);
}