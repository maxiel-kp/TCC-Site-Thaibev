using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using TCC_No2_API.DTOs;
using TCC_No2_API.Entities;
using TCC_No2_API.Repositories;

namespace TCC_No2_API.Services
{
    public class UserAuthenService : IUserAuthenService
    {
        private readonly IUserAuthenRepository _repository;

        public UserAuthenService(IUserAuthenRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var passwordHasher = new PasswordHasher<UserAuthen>();

            var passwordHash = passwordHasher.HashPassword(null, request.Password);

            var userAuthen = new UserAuthen
            {
                Username = request.Username,
                PasswordHash = passwordHash
            };

            return await _repository.RegisterAsync(userAuthen);
        }
    }
}
