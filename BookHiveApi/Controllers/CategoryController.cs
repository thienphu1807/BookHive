using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHiveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {

            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategory category)
        {
            var result = _categoryService.AddCategory(category);
            return result ? Ok() : BadRequest();
        }
        [HttpPut("{id}")]
        public  IActionResult Update(int id, [FromBody] CreateCategory category)
        {
            if (!_categoryService.HasCategory(id))
            {
                return NotFound();
            }
            var exsitCategory = _categoryService.GetCategoryById(id);
            _mapper.Map(category, exsitCategory);
            if (!_categoryService.UpdateCategory(exsitCategory))
            {
                return StatusCode(500, "something wrong when update");
            }
            return Ok(exsitCategory);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_categoryService.HasCategory(id))
            {
                return NotFound();
            }
            if(!_categoryService.DeleteCategory(id))
            {
                return StatusCode(500, "something wrong when delete");
            }
            return Ok();
        }
    }
}
