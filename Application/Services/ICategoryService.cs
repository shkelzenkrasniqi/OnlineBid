using Domain.DTOs;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategorysAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(Guid id);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO CategoryDTO);
        Task<bool> UpdateCategoryAsync(Guid id, CategoryDTO CategoryDTO);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
