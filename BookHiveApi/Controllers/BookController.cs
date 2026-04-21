using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dto;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public BookController(BookService bookService, CategoryService categoryService, AuthorService authorService, IMapper mapper, UserManager<User> userManager)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _mapper = mapper;
            _userManager = userManager;
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
        public IActionResult GetBookFromTilte(string title)
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
        public async Task<IActionResult> RateBook(int bookId, [FromBody] AddBookReview addBookReview)
        {
            var userName = Request.Headers["X-UserName"].FirstOrDefault();

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Missing username header");
            }

            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return Unauthorized();
            }

            var success = _bookService.AddBookRating(bookId, user.Id, addBookReview);
            if (success)
            {
                var reviews = await _bookService.GetBookRating(bookId);
                return Ok(reviews);
            }

            return BadRequest("Could not add review");
        }
        [HttpGet("{bookId}/review")]
        public async Task<IActionResult> GetBookReview(int bookId)
        {
            var result = await _bookService.GetBookRating(bookId);
            return result != null ? Ok(result) : BadRequest();
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
