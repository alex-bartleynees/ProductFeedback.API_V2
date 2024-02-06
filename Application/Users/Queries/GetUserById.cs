using Application.Abstractions;
using Application.Common;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries
{
    public record GetUserById(int userId) : IRequest<Result<User>>;
    public class GetUserByIdHandler : IRequestHandler<GetUserById, Result<User>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        public GetUserByIdHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }

        public async Task<Result<User>> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            return await _suggestionsRepository.GetUser(request.userId);
        }
    }
}