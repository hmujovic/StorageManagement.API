namespace Services.Abstractions;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDto>> GetAll(CancellationToken cancellationToken = default);

    Task<OrderItemDto> GetById(int orderItemId, CancellationToken cancellationToken = default);

    Task<IEnumerable<OrderItemDto>> GetByOrderId(int orderId, CancellationToken cancellationToken = default);

    Task Delete(int orderItemId, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Create(OrderItemCreateDto orderItemDto, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Update(int orderItemId, OrderItemUpdateDto orderItemDto, CancellationToken cancellationToken = default);
}