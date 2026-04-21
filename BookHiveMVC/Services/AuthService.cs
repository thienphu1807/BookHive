using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Models.Dtos;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookHiveMVC.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        private readonly string apiPath = ApiEndpoints.AuthAPIPath;

        public AuthService(IAuthRepository authRepo, IMapper mapper)
        {
            _authRepo = authRepo;
            _mapper = mapper;
        }
        public async Task<bool> Register(RegisterDto registerDto)
        {
            return await _authRepo.RegisterAsync(apiPath + "register/", registerDto);
        }
        public async Task<bool> LogOut()
        {
            return await _authRepo.LogOutAsync(apiPath + "logout/");
        }
        public async Task<ResponseAuth?> SignIn(LoginDto loginDto)
        {
            return await _authRepo.SignInAsync(apiPath + "login/", loginDto);
        }
    }
}
