using Domain.Entities;

namespace Services;

public class OrderItemService(IRepositoryManager repositoryManager) : IOrderItemService
{
    public async Task<GeneralResponseDto> Create(OrderItemCreateDto orderItemDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var orderItem = orderItemDto.Adapt<OrderItem>();
            orderItem.Product = null;

            var product = await repositoryManager.ProductRepository.GetById(orderItem.ProductId, cancellationToken);
            if (product == null)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            if (product.Quantity < orderItem.Quantity)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Not enough product in stock!"
                };
            }

            product.Quantity -= orderItem.Quantity;
            repositoryManager.ProductRepository.Update(product);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            orderItem.Price = product.Price * orderItemDto.Quantity;
            repositoryManager.OrderItemRepository.CreateOrderItem(orderItem);
            var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (rowsAffected != 1)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Error!"
                };
            }

            return new GeneralResponseDto { Data = orderItem.Id, Message = "Success!" };
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

    public async Task Delete(int orderItemId, CancellationToken cancellationToken = default)
    {
        var orderItem = await repositoryManager.OrderItemRepository.GetById(orderItemId, cancellationToken);
        repositoryManager.OrderItemRepository.DeleteOrderItem(orderItem, cancellationToken);
        await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderItemDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var orderItems = await repositoryManager.OrderItemRepository.GetAll(cancellationToken);
        return orderItems.Adapt<IEnumerable<OrderItemDto>>();
    }

    public async Task<IEnumerable<OrderItemDto>> GetByOrderId(int orderId, CancellationToken cancellationToken = default)
    {
        var orderItems = await repositoryManager.OrderItemRepository.GetByOrderId(orderId, cancellationToken);
        return orderItems.Adapt<IEnumerable<OrderItemDto>>();
    }

    public async Task<OrderItemDto> GetById(int orderItemId, CancellationToken cancellationToken = default)
    {
        var orderItem = await repositoryManager.OrderItemRepository.GetById(orderItemId, cancellationToken);
        return orderItem.Adapt<OrderItemDto>();
    }

    public async Task<GeneralResponseDto> Update(int orderItemId, OrderItemUpdateDto orderItemDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingOrderItem = await repositoryManager.OrderItemRepository.GetById(orderItemId, cancellationToken);
            if (existingOrderItem == null)
                return new GeneralResponseDto { IsSuccess = false, Message = "Order Item not found." };

            var product = await repositoryManager.ProductRepository.GetById(existingOrderItem.ProductId, cancellationToken);

            if (product == null)
            {
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            if (orderItemDto.Quantity > existingOrderItem.Quantity)
            {
                var qtyDifference = orderItemDto.Quantity - existingOrderItem.Quantity;
                if (product.Quantity < qtyDifference || product.Quantity == 0)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product out of stock."
                    };
                }
                product.Quantity -= qtyDifference;
            }
            else if (orderItemDto.Quantity < existingOrderItem.Quantity)
            {
                var qtyDifference = existingOrderItem.Quantity - orderItemDto.Quantity;
                product.Quantity += qtyDifference;
            }
            // update product
            repositoryManager.ProductRepository.Update(product);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            // update orderItem
            orderItemDto.Adapt(existingOrderItem);
            existingOrderItem.Product = null;
            repositoryManager.OrderItemRepository.UpdateOrderItem(existingOrderItem);
            var res = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (res != 1)
                return new GeneralResponseDto { IsSuccess = false };

            return new GeneralResponseDto { Data = existingOrderItem.Id, Message = "Success!" };
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