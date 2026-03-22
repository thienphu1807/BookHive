using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookHiveApi.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthService(IAuthRepository authRepo, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _authRepo = authRepo;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> Register(RegisterDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _authRepo.RegisterUser(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Reader");
            }
            return result;
        }
        public async Task<ResponseAuth?> Login(LoginDto dto)
        {
            var user = await _authRepo.GetUserByEmail(dto.Email);
            if (user == null) return null;

            var result = await _authRepo.CheckPassword(user, dto.Password);
            var reponseAuth = _mapper.Map<ResponseAuth>(user);
            return result.Succeeded ? reponseAuth : null;
        }
        public async Task<ResponseAuth> UpdatePassword(UpdatePasswordDto dto)
        {
            var user = await _authRepo.GetUserByEmail(dto.Email);
            if (user == null)
            {
                return null;
            }

            var result =  await _authRepo.ChangePassword(user, dto.OldPassword, dto.NewPassword);
            if(!result.Succeeded)
            {
                return null;
            }

            var reponseAuth = _mapper.Map<ResponseAuth>(user);

            return reponseAuth;
        }
        public async Task<ResponseAuth> UpdateRole(string userEmail)
        {
            var user = await _authRepo.GetUserByEmail(userEmail);
            if (user == null)
            {
                return null;
            }

            await _userManager.RemoveFromRoleAsync(user, "Reader");
            var result = await _userManager.AddToRoleAsync(user, "Reviewer");

            if (!result.Succeeded)
            {
                return null;
            }


            var reponseAuth = _mapper.Map<ResponseAuth>(user);

            return reponseAuth;
        }
    }
}
