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
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(AuthorService authorService, IMapper mapper)
        {

            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authors = _authorService.GetAllAuthor();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthor author)
        {
            var result = _authorService.AddAuthor(author);
            return result ? Ok() : BadRequest();
        }
        [HttpPut("{id}")]
        public  IActionResult Update(int id, [FromBody] CreateAuthor author)
        {
            if (!_authorService.HasAuthor(id))
            {
                return NotFound();
            }
            var exsitAuthor = _authorService.GetAuthorById(id);
            _mapper.Map(author, exsitAuthor);
            if (!_authorService.UpdateAuthor(exsitAuthor))
            {
                return StatusCode(500, "something wrong when update");
            }
            return Ok(exsitAuthor);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_authorService.HasAuthor(id))
            {
                return NotFound();
            }
            if(!_authorService.DeleteAuthor(id))
            {
                return StatusCode(500, "something wrong when delete");
            }
            return Ok();
        }
    }
}
