using Application.Common;
using Application.Common.Models;

namespace Application.Abstractions
{
    public interface IKeycloakService
    {
        Task<Result<KeycloakUserResponse>> GetUserByEmailAsync(string email);
        Task<Result<KeycloakUserResponse>> CreateUserAsync(UserForCreationDto user);
    }
}
