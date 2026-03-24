using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository;
using BookHiveApi.Repository.IRepository;

namespace BookHiveApi.Services
{
    public class CategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public ICollection<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }
        public Category GetCategoryById(int id)
        {
            return _categoryRepository.Get(id);
        }
        public bool AddCategory(CreateCategory categoryDtos)
        {
            var category = _mapper.Map<Category>(categoryDtos);
            return _categoryRepository.Add(category);
        }
        public bool UpdateCategory(Category category)
        {
            return _categoryRepository.Update(category);
        }
        public bool DeleteCategory(int id)
        {
            return _categoryRepository.Delete(id);
        }
        public bool HasCategory(int id)
        {
            return _categoryRepository.HasValue(id);
        }
        public ICollection<Book> GetBooksFromCategory(int categoryId)
        {
            return _categoryRepository.GettBookFromCategory(categoryId);
        }
    }
}
