using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CategoryDTO>> GetAllCategorysAsync()
        {
            var categorys = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categorys);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO CategoryDTO)
        {
            var category = _mapper.Map<Category>(CategoryDTO);

            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, CategoryDTO CategoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            _mapper.Map(CategoryDTO, category);
            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepository.DeleteAsync(id);
            return true;
        }
       
    }
}