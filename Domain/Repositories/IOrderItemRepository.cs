using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderItemRepository : IRepositoryBase<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetAll(CancellationToken cancellationToken = default);

    Task<IEnumerable<OrderItem>> GetByOrderId(int orderId, CancellationToken cancellationToken = default);

    Task<OrderItem> GetById(int orderItemId, CancellationToken cancellationToken = default);

    void CreateOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default);

    void DeleteOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default);

    void UpdateOrderItem(OrderItem orderItem, CancellationToken cancellationToken = default);
}