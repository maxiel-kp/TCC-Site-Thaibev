using Microsoft.EntityFrameworkCore;
using TCC_No2_API.Data;
using TCC_No2_API.DTOs;
using TCC_No2_API.Entities;

namespace TCC_No2_API.Repositories
{
    public class UserAuthenRepository : IUserAuthenRepository
    {
        private readonly AppDbContext _dbContext;

        public UserAuthenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegisterAsync(UserAuthen userAuthen)
        {
            var hasUsername = await _dbContext.UserAuthens.AnyAsync(a => a.Username == userAuthen.Username);
            if(hasUsername)
            {
                return false;
            }

            await _dbContext.UserAuthens.AddAsync(userAuthen);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
