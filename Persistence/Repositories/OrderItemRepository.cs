using Domain.Repositories;

namespace Persistence.Repositories
{
    public class OrderItemRepository(DataContext dataContext) : RepositoryBase<OrderItem>(dataContext), IOrderItemRepository
    {
        public void CreateOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default) => Create(orderItem);

        public void DeleteOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default) => Delete(orderItem);

        public void UpdateOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default) => Update(orderItem);

        public async Task<IEnumerable<OrderItem>> GetAll(CancellationToken cancellationToken = default)
        {
            return await FindAll().ToListAsync(cancellationToken);
        }

        public async Task<OrderItem> GetById(int orderItemId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.Id == orderItemId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderId(int orderId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(p => p.OrderId == orderId)
                .ToListAsync(cancellationToken);
        }
    }
}