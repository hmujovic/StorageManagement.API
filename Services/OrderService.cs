namespace Services;

public class OrderService(IRepositoryManager repositoryManager) : IOrderService
{
    public async Task<GeneralResponseDto> Create(OrderCreateDto orderDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var order = orderDto.Adapt<Order>();
            order.Customer = null;
            repositoryManager.OrderRepository.CreateOrder(order);
            var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (rowsAffected != 1)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Error!"
                };
            }

            return new GeneralResponseDto { Data = order.Id, Message = "Success!" };
        }
        catch (Exception ex)
        {
            return new GeneralResponseDto
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task Delete(int orderId, CancellationToken cancellationToken = default)
    {
        var order = await repositoryManager.OrderRepository.GetById(orderId, cancellationToken);
        repositoryManager.OrderRepository.DeleteOrder(order, cancellationToken);
        await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var orders = await repositoryManager.OrderRepository.GetAll(cancellationToken);
        return orders.Adapt<IEnumerable<OrderDto>>();
    }

    public async Task<IEnumerable<OrderDto>> GetByCustomerId(string customerId, CancellationToken cancellationToken = default)
    {
        var orders = await repositoryManager.OrderRepository.GetByCustomerId(customerId, cancellationToken);
        return orders.Adapt<IEnumerable<OrderDto>>();
    }

    public async Task<OrderDto> GetById(int orderId, CancellationToken cancellationToken = default)
    {
        var order = await repositoryManager.OrderRepository.GetById(orderId, cancellationToken);
        return order.Adapt<OrderDto>();
    }

    public async Task<GeneralResponseDto> Update(int orderId, OrderUpdateDto orderDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingOrder = await repositoryManager.OrderRepository.GetById(orderId, cancellationToken);
            if (existingOrder == null)
                return new GeneralResponseDto { IsSuccess = false, Message = "Order not found." };

            orderDto.Adapt(existingOrder);
            existingOrder.Customer = null;
            repositoryManager.OrderRepository.UpdateOrder(existingOrder);
            var res = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (res != 1)
                return new GeneralResponseDto { IsSuccess = false };

            return new GeneralResponseDto { Data = existingOrder.Id, Message = "Success!" };
        }
        catch (Exception ex)
        {
            return new GeneralResponseDto
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}