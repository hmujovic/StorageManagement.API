namespace StorageManagement.API.Controllers
{
    [ApiController]
    [Route("api/order-items")]
    public class OrderItemController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems(CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderItemService.GetAll(cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{orderItemId}")]
        public async Task<IActionResult> Delete(int orderItemId, CancellationToken cancellationToken)
        {
            await serviceManager.OrderItemService.Delete(orderItemId, cancellationToken);
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderItemCreateDto OrderItemDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderItemService.Create(OrderItemDto, cancellationToken);
            return Ok(response);
        }

        [HttpGet("details/{orderItemId}")]
        public async Task<IActionResult> GetOrderItemById(int orderItemId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderItemService.GetById(orderItemId, cancellationToken);
            return Ok(response);
        }

        [HttpGet("byOrder/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderItemService.GetByOrderId(orderId, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(int orderItemId, [FromBody] OrderItemUpdateDto OrderItemDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderItemService.Update(orderItemId, OrderItemDto, cancellationToken);
            return Ok(response);
        }
    }
}