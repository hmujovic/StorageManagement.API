using Domain.Entities;

namespace Domain.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<IEnumerable<Account>> GetAll(CancellationToken cancellationToken = default);

    Task<Account> GetById(string accountId, CancellationToken cancellationToken = default);
}