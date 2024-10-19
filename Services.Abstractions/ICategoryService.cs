using Contract;

namespace Services.Abstractions;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken = default);

    Task<CategoryDto> GetById(int CategoryId, CancellationToken cancellationToken = default);

    Task Delete(int CategoryId, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Create(CategoryCreateDto CategoryDto, CancellationToken cancellationToken = default);

    Task<GeneralResponseDto> Update(int CategoryId, CategoryUpdateDto CategoryDto, CancellationToken cancellationToken = default);
}
