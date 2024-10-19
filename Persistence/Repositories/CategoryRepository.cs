using Domain.Repositories;

namespace Persistence.Repositories
{
    public class CategoryRepository(DataContext dataContext) : RepositoryBase<Category>(dataContext), ICategoryRepository
    {
        public void CreateCategory(Category category, CancellationToken cancellationToken = default) => Create(category);

        public void DeleteCategory(Category category, CancellationToken cancellationToken = default) => Delete(category);

        public void UpdateCategory(Category category, CancellationToken cancellationToken = default) => Update(category);

        public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken = default)
        {
            return await FindAll().ToListAsync(cancellationToken);
        }

        public async Task<Category> GetById(int cartegoryId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(category => category.Id == cartegoryId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}