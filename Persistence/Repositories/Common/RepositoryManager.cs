using Domain.Repositories;

namespace Persistence.Repositories
{
    public class RepositoryManager(DataContext dbContext) : IRepositoryManager
    {
        private readonly Lazy<IAccountRepository> _lazyAccountRepository = new(() => new AccountRepository(dbContext));

        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork = new(() => new UnitOfWork(dbContext));

        private readonly Lazy<IProductRepository> _lazyProductRepository = new(() => new ProductRepository(dbContext));

        private readonly Lazy<ICategoryRepository> _lazyCategoryRepository = new(() => new CategoryRepository(dbContext));


        public IAccountRepository AccountRepository => _lazyAccountRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
        public IProductRepository ProductRepository => _lazyProductRepository.Value;
        public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;
    }
}