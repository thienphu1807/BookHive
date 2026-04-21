using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;
using BookHiveMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookHiveMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly IMapper _mapper;
        public AccountController(AuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }
            var result = await _authService.Register(registerDto);
            if (result)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Register");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var userResponse = await _authService.SignIn(loginDto);
            if (userResponse != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userResponse.Email),
                    new Claim(ClaimTypes.Role, userResponse.Role)
                 };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
                await HttpContext.SignOutAsync("Cookies");
                return RedirectToAction("Index", "Home");
        }
    }
}