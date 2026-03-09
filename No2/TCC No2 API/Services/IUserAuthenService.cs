using TCC_No2_API.DTOs;

namespace TCC_No2_API.Services
{
    public interface IUserAuthenService
    {
        Task<bool> Register(RegisterRequest request);
    }
}