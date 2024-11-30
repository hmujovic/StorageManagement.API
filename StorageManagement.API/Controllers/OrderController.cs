namespace StorageManagement.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderService.GetAll(cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(int orderId, CancellationToken cancellationToken)
        {
            await serviceManager.OrderService.Delete(orderId, cancellationToken);
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderService.Create(orderDto, cancellationToken);
            return Ok(response);
        }

        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderService.GetById(orderId, cancellationToken);
            return Ok(response);
        }

        [HttpGet("byCustomer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(string customerId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderService.GetByCustomerId(customerId, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderUpdateDto OrderDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.OrderService.Update(orderId, OrderDto, cancellationToken);
            return Ok(response);
        }
    }
}