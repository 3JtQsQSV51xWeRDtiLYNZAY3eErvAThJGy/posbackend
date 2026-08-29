using System;
using System.Threading.Tasks;
using posbackend.DTOs;

namespace posbackend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<UserProfileDto?> GetUserProfileByIdAsync(Guid userId);
    }
}
