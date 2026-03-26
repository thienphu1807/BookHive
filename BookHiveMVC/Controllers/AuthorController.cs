using AutoMapper;
using BookHiveApi.Services;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookHiveMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorController(AuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllAuthor()
        {
            var authors = await _authorService.GetAllAuthor();
            return View(authors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthor createAuthor)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            await _authorService.AddAuthor(createAuthor);

            return RedirectToAction("GetAllAuthor");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        //POST: TasksController/Edit
       [HttpPost]
        public async Task<IActionResult> Edit(int id, Author author)
        {

            //var editAuthor = _mapper.Map<CreateAuthor>(author);
            await _authorService.UpdateAuthor(id, author);
            return RedirectToAction("GetAllAuthor");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _authorService.DeleteAuthor(id);

            return RedirectToAction("GetAllAuthor");
        }
    }
}