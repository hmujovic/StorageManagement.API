namespace Services
{
    public class CategoryService(IRepositoryManager repositoryManager) : ICategoryService
    {
        public async Task<GeneralResponseDto> Create(CategoryCreateDto CategoryDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var Category = CategoryDto.Adapt<Category>();
                repositoryManager.CategoryRepository.CreateCategory(Category);
                var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (rowsAffected != 1)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Error!"
                    };
                }

                return new GeneralResponseDto { Message = "Success!" };
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

        public async Task Delete(int CategoryId, CancellationToken cancellationToken = default)
        {
            var Category = await repositoryManager.CategoryRepository.GetById(CategoryId, cancellationToken);
            repositoryManager.CategoryRepository.DeleteCategory(Category, cancellationToken);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var categories = await repositoryManager.CategoryRepository.GetAll(cancellationToken);
            return categories.Adapt<IEnumerable<CategoryDto>>();
        }

        public async Task<CategoryDto> GetById(int CategoryId, CancellationToken cancellationToken = default)
        {
            var Category = await repositoryManager.CategoryRepository.GetById(CategoryId, cancellationToken);
            return Category.Adapt<CategoryDto>();
        }

        public async Task<GeneralResponseDto> Update(int CategoryId, CategoryUpdateDto CategoryDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingCategory = await repositoryManager.CategoryRepository.GetById(CategoryId, cancellationToken);
                if (existingCategory == null)
                    return new GeneralResponseDto { IsSuccess = false, Message = "Category not found." };

                CategoryDto.Adapt(existingCategory);

                repositoryManager.CategoryRepository.UpdateCategory(existingCategory);
                var res = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (res != 1)
                    return new GeneralResponseDto { IsSuccess = false };

                return new GeneralResponseDto { Message = "Success!" };
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
}