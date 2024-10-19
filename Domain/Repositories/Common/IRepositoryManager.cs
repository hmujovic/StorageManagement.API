namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}