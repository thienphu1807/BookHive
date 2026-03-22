using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookHiveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public BookController(BookService bookService, CategoryService categoryService, AuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var book = _bookService.GetAllBook();
            var getBook = _mapper.Map<ICollection<GetBook>>(book);
            return Ok(getBook);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet("author/{id}")]
        public IActionResult GetBookFromAuthor(int id)
        {
            var book = _authorService.GetBooksFromAuthor(id);
            if (book == null)
            {
                return NotFound();
            }
            var getBook = _mapper.Map<ICollection<GetBook>>(book);
            return Ok(getBook);
        }
        [HttpGet("category/{id}")]
        public IActionResult GetBookFromCategory(int id)
        {
            var book = _categoryService.GetBooksFromCategory(id);
            if (book == null)
            {
                return NotFound();
            }
            var getBook = _mapper.Map<ICollection<GetBook>>(book);
            return Ok(getBook);
        }
        [HttpGet("title")]
        public IActionResult GetBookFromCategory(string title)
        {
            var book = _bookService.GetBookByTitle(title);
            if (book == null)
            {
                return NotFound();
            }
            var getBook = _mapper.Map<ICollection<GetBook>>(book);
            return Ok(getBook);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateBook book)
        {
            var result = _bookService.AddBook(book);
            return result ? Ok() : BadRequest();
        }
        [HttpPost("{bookId}/review")]
        public IActionResult RateBook(int bookId, [FromBody] AddBookReview addBookReview)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            var result = _bookService.AddBookRating(bookId, userId, addBookReview)
            return result ? Ok() : BadRequest();
        }
        [HttpPut("{id}")]
        public  IActionResult Update(int id, [FromBody] CreateBook book)
        {
            if (!_bookService.HasBook(id))
            {
                return NotFound();
            }
            var exsitBook = _bookService.GetBookById(id);
            _mapper.Map(book, exsitBook);
            if (!_bookService.UpdateBook(exsitBook))
            {
                return StatusCode(500, "something wrong when update");
            }
            return Ok(exsitBook);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_bookService.HasBook(id))
            {
                return NotFound();
            }
            if(!_bookService.DeleteBook(id))
            {
                return StatusCode(500, "something wrong when delete");
            }
            return Ok();
        }
    }
}
