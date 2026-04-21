using AspNetCoreGeneratedDocument;
using AutoMapper;
using BookHiveMVC.Controllers;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;
using BookHiveMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHiveMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;
        private readonly IMapper _mapper;
        public BookController(BookService bookService, IMapper mapper, CategoryService categoryService, AuthorService authorService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _categoryService = categoryService;
            _authorService = authorService;
        }
        public async Task<IActionResult> GetAllBook(string categoryName, int page = 1, int pageSize = 10)
        {
            var books = await _bookService.GetAllBook();
            var getBook = _mapper.Map<ICollection<GetBook>>(books);

            if (!string.IsNullOrEmpty(categoryName))
            {
                getBook = getBook
                    .Where(b => b.CategoryNames.Contains(categoryName))
                    .ToList();
            }
            var categories = await _categoryService.GetAllCategory();
            ViewBag.Categories = categories;

            int totalItems = getBook.Count;

            var pagedBooks = getBook
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedBooks);
        }
        public async Task<IActionResult> GetAllBookFromAuthor(int authorId, string categoryName, int page = 1, int pageSize = 10)
        {
            var books = await _bookService.GetBookFromAuthor(authorId);
            var getBook = _mapper.Map<ICollection<GetBook>>(books);

            if (!string.IsNullOrEmpty(categoryName))
            {
                getBook = getBook
                    .Where(b => b.CategoryNames.Contains(categoryName))
                    .ToList();
            }
            ViewBag.AuthorId = authorId;

            var categories = await _categoryService.GetAllCategory();
            ViewBag.Categories = categories;

            int totalItems = getBook.Count;

            var pagedBooks = getBook
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedBooks);
        }
        public async Task<IActionResult> GetBookRating(int bookId)
        {
            var books = await _bookService.GetBookReview(bookId);
            ViewBag.BookId = bookId;
            return View(books);
        }
        public async Task<IActionResult> AddBookRating(int bookId, AddBookReview addBookReview)
        {
            var success = await _bookService.AddBookRating(bookId, addBookReview);
            if (success)
            {
                var books = await _bookService.GetBookReview(bookId);
                return Ok(books);
            }
            return BadRequest("Could not add review");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategory();
            if (categories != null)
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
            }
            var authors = await _authorService.GetAllAuthor();
            if (authors != null)
            {
                ViewBag.Authors = new SelectList(authors, "Id", "Name");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBook createBook)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            await _bookService.AddBook(createBook);

            return RedirectToAction("GetAllBook");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _bookService.GetBookById(id);
            return View(author);
        }

        //POST: TasksController/Edit
       [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            await _bookService.UpdateBook(id, book);
            return RedirectToAction("GetAllBook");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _bookService.DeleteBook(id);

            return RedirectToAction("GetAllBook");
        }
    }
}