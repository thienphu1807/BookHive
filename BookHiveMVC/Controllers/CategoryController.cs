using AutoMapper;
using BookHiveMVC.Services;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookHiveMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategory();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategory createCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            await _categoryService.AddCategory(createCategory);

            return RedirectToAction("GetAllCategories");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return View(category);
        }

        //POST: TasksController/Edit
       [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {

            var EditCategory = _mapper.Map<CreateCategory>(category);
            await _categoryService.UpdateCategory(id, EditCategory);
            return RedirectToAction("GetAllCategories");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _categoryService.DeleteCategory(id);

            return RedirectToAction("GetAllCategories");
        }
    }
}