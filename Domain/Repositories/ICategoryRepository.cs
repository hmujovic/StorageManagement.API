using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken = default);

    Task<Category> GetById(int cartegoryId, CancellationToken cancellationToken = default);

    void CreateCategory(Category category, CancellationToken cancellationToken = default);

    void DeleteCategory(Category category, CancellationToken cancellationToken = default);

    void UpdateCategory(Category category, CancellationToken cancellationToken = default);
}