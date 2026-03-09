using TCC_No2_API.DTOs;
using TCC_No2_API.Entities;

namespace TCC_No2_API.Repositories
{
    public interface IUserAuthenRepository
    {
        public Task<bool> RegisterAsync(UserAuthen request);
    }
}