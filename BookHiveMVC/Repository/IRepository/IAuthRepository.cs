using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BookHiveMVC.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<ResponseAuth?> SignInAsync(string url, LoginDto loginDto);
        Task<bool> LogOutAsync(string url);
        Task<bool> RegisterAsync(string url, RegisterDto registerDto);

    }
}
