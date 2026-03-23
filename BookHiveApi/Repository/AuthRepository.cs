using BookHiveApi.Models;
using BookHiveApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace BookHiveApi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<SignInResult> CheckPassword(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> ChangePassword(User user, string oldPass, string newPass)
        {
            return await _userManager.ChangePasswordAsync(user, oldPass, newPass);
        }
        public async Task<SignInResult> LoginUser(string email, string password, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure: false);
        }
    }
}
