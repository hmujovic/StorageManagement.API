namespace Services.Abstractions;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken = default);

    Task<OrderDto> GetById(int orderId, CancellationToken cancellationToken = default);

    Task<IEnumerable<OrderDto>> GetByCustomerId(string customerId, CancellationToken cancellationToken = default);

    Task Delete(int orderId, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Create(OrderCreateDto orderDto, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Update(int orderId, OrderUpdateDto orderDto, CancellationToken cancellationToken = default);
}