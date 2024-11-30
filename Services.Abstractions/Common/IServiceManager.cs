global using Contract;
global using System.Security.Claims;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IAccountService AccountService { get; }

        IProductService ProductService { get; }

        ICategoryService CategoryService { get; }
        IOrderService OrderService { get; }
        IOrderItemService OrderItemService { get; }
    }
}