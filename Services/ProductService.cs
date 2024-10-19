using Contract;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services
{
    public class ProductService(IRepositoryManager repositoryManager) : IProductService
    {
        public async Task<GeneralResponseDto> Create(ProductCreateDto productDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = productDto.Adapt<Product>();
                repositoryManager.ProductRepository.CreateProduct(product);
                var rowsAffected = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (rowsAffected != 1)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Error!"
                    };
                }

                foreach (var categoryId in productDto.ProductCategories) 
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    };

                    repositoryManager.ProductCategoryRepository.Create(productCategory);
                    await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
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

        public async Task Delete(int productId, CancellationToken cancellationToken = default)
        {
            var product = await repositoryManager.ProductRepository.GetById(productId, cancellationToken);
            repositoryManager.ProductRepository.DeleteProduct(product, cancellationToken);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var products = await repositoryManager.ProductRepository.GetAll(cancellationToken);
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken = default)
        {
            var products = await repositoryManager.ProductRepository.GetByCategoryId(categoryId, cancellationToken);
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        public async Task<ProductDto> GetById(int productId, CancellationToken cancellationToken = default)
        {
            var product = await repositoryManager.ProductRepository.GetById(productId, cancellationToken);
            return product.Adapt<ProductDto>();
        }

        public async Task<GeneralResponseDto> Update(int productId, ProductUpdateDto productDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingProduct = await repositoryManager.ProductRepository.GetById(productId, cancellationToken);
                if (existingProduct == null)
                    return new GeneralResponseDto { IsSuccess = false, Message = "Product not found." };

                productDto.Adapt(existingProduct);

                repositoryManager.ProductRepository.UpdateProduct(existingProduct);
                var res = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (res != 1)
                    return new GeneralResponseDto { IsSuccess = false };

                var existingCategories = await repositoryManager.ProductCategoryRepository.GetByProductId(productId);
                foreach (var categoryId in productDto.ProductCategories)
                {
                    var missingCategories = existingCategories
                    .Where(ec => !productDto.ProductCategories
                        .Any(pc => pc == ec.Id))
                    .ToList();

                    foreach (var cat in missingCategories)
                    {
                        repositoryManager.ProductCategoryRepository.Delete(cat);
                        await repositoryManager.UnitOfWork.SaveChangesAsync();
                    }

                    var category = existingCategories.FirstOrDefault(c => c.CategoryId == categoryId);
                    if (category == null)
                    {
                        var productCategory = new ProductCategory
                        {
                            CategoryId = categoryId,
                            ProductId = existingProduct.Id
                        };
                        repositoryManager.ProductCategoryRepository.Create(productCategory);
                        await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    if (category != null)
                    {
                        continue;
                    }
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
    }
}
