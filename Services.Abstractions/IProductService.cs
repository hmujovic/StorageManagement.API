using Contract;

namespace Services.Abstractions;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAll(CancellationToken cancellationToken = default);

    Task<ProductDto> GetById(int productId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProductDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken = default);

    Task Delete(int productId, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Create(ProductCreateDto productDto, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Update(int productId, ProductUpdateDto productDto, CancellationToken cancellationToken = default);
}
