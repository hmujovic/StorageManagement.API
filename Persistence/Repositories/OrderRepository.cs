using Domain.Repositories;

namespace Persistence.Repositories
{
    public class OrderRepository(DataContext dataContext) : RepositoryBase<Order>(dataContext), IOrderRepository
    {
        public void CreateOrder(Order order, CancellationToken cancellationToken = default) => Create(order);

        public void DeleteOrder(Order order, CancellationToken cancellationToken = default) => Delete(order);

        public void UpdateOrder(Order order, CancellationToken cancellationToken = default) => Update(order);

        public async Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken = default)
        {
            return await FindAll().ToListAsync(cancellationToken);
        }

        public async Task<Order> GetById(int orderId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.Id == orderId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetByCustomerId(string customerId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }
    }
}