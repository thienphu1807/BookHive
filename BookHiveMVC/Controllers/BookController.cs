using AutoMapper;
using BookHiveApi.Services;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookHiveMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly IMapper _mapper;
        public BookController(BookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllBook()
        {
            var books = await _bookService.GetAllBook();
            var getBook = _mapper.Map<ICollection<GetBook>>(books);
            return View(getBook);
        }
        [HttpGet]
        public IActionResult Create()
        {
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