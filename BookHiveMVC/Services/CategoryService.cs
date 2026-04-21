using AutoMapper;
using BookHiveMVC;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveMVC.Services
{
    public class CategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _categoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<Category>> GetAllCategory()
        {
            return await _categoryRepository.GetAllAsync(ApiEndpoints.CategoryAPIPath);
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetAsync(ApiEndpoints.CategoryAPIPath, id);
        }
        public async Task<bool> AddCategory(CreateCategory CategoryDtos)
        {
            var Category = _mapper.Map<Category>(CategoryDtos);
            return await _categoryRepository.CreateAsync(ApiEndpoints.CategoryAPIPath, Category);
        }
        public async Task<bool> UpdateCategory(int id, CreateCategory CategoryDtos)
        {
            var Category = _mapper.Map<Category>(CategoryDtos);
            return await _categoryRepository.UpdateAsync(ApiEndpoints.CategoryAPIPath + id.ToString(), Category);
        }
        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoryRepository.DeleteAsync(ApiEndpoints.CategoryAPIPath, id);
        }
    }
}
