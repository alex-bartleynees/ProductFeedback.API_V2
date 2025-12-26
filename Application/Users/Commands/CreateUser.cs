using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands
{
    public record CreateUser(UserForCreationDto User) : IRequest<Result<User>>;

    public class CreateUserHandler : IRequestHandler<CreateUser, Result<User>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateUserHandler(ISuggestionsRepository suggestionsRepository, IKeycloakService keycloakService)
        {
            _suggestionsRepository = suggestionsRepository ?? throw new ArgumentNullException(nameof(suggestionsRepository));
            _keycloakService = keycloakService ?? throw new ArgumentNullException(nameof(keycloakService));
        }

        public async Task<Result<User>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);
            Guard.Against.Null(request.User);

            // 1. Check if user exists in local DB
            var localUserResult = await _suggestionsRepository.GetUserByEmailAsync(request.User.Email);
            if (localUserResult.IsSuccess)
            {
                return Result<User>.Failure(new Error(409, "Conflict", $"User with email {request.User.Email} already exists"));
            }

            // 2. Check if user exists in Keycloak
            var keycloakUserResult = await _keycloakService.GetUserByEmailAsync(request.User.Email);

            if (keycloakUserResult.IsSuccess)
            {
                // User exists in Keycloak but not in local DB - sync to local DB
                var keycloakUser = keycloakUserResult.Value!;
                var syncedUser = new User(
                    $"{keycloakUser.FirstName} {keycloakUser.LastName}".Trim(),
                    keycloakUser.Username,
                    request.User.Image)
                {
                    Id = Guid.Parse(keycloakUser.Id),
                    Email = keycloakUser.Email
                };

                var createResult = await _suggestionsRepository.CreateUserAsync(syncedUser);
                if (createResult.IsFailure)
                {
                    return Result<User>.Failure(createResult.Error);
                }

                return Result<User>.Success(createResult.Value!);
            }

            // 3. User doesn't exist in Keycloak - create in both
            var createKeycloakResult = await _keycloakService.CreateUserAsync(request.User);
            if (createKeycloakResult.IsFailure)
            {
                return Result<User>.Failure(createKeycloakResult.Error);
            }

            var createdKeycloakUser = createKeycloakResult.Value!;
            var newUser = new User(request.User.Name, request.User.Username, request.User.Image)
            {
                Id = Guid.Parse(createdKeycloakUser.Id),
                Email = request.User.Email
            };

            var localCreateResult = await _suggestionsRepository.CreateUserAsync(newUser);
            if (localCreateResult.IsFailure)
            {
                return Result<User>.Failure(localCreateResult.Error);
            }

            return Result<User>.Success(localCreateResult.Value!);
        }
    }
}
