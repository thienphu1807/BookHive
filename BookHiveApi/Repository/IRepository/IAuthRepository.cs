using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BookHiveApi.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(User user, string password);
        Task<SignInResult> CheckPassword(User user, string password);
        Task<User> GetUserByEmail(string email);
        Task<IdentityResult> ChangePassword(User user, string oldPass, string newPass);
    }
}
