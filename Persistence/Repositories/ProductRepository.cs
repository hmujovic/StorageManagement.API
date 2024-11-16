using Domain.Repositories;

namespace Persistence.Repositories
{
    public class ProductRepository(DataContext dataContext) : RepositoryBase<Product>(dataContext), IProductRepository
    {
        public void CreateProduct(Product product, CancellationToken cancellationToken = default) => Create(product);

        public void DeleteProduct(Product product, CancellationToken cancellationToken = default) => Delete(product);

        public void UpdateProduct(Product product, CancellationToken cancellationToken = default) => Update(product);

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
        {
            return await FindAll().ToListAsync(cancellationToken);
        }

        public async Task<Product> GetById(int productId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.Id == productId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(int categoryId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }
    }
}