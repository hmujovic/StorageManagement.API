using Domain.Repositories;

namespace Persistence.Repositories
{
    public class ProductCategoryRepository(DataContext dataContext) : RepositoryBase<ProductCategory>(dataContext), IProductCategoryRepository
    {
        public void CreateProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default) => Create(productCategory);

        public void DeleteProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default) => Delete(productCategory);

        public void UpdateProductCategory(ProductCategory productCategory, CancellationToken cancellationToken = default) => Update(productCategory);

        public async Task<IEnumerable<ProductCategory>> GetAll(CancellationToken cancellationToken = default)
        {
            return await FindAll().ToListAsync(cancellationToken);
        }

        public async Task<ProductCategory> GetById(int productCategoryId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.Id == productCategoryId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductCategory>> GetByProductId(int productId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(pc => pc.ProductId == productId)
                .ToListAsync(cancellationToken);
        }
    }
}