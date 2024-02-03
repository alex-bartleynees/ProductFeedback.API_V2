using Application.Abstractions;
using Application.Common;
using DataAccess.DbContexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly SuggestionContext _context;

        public SuggestionsRepository(SuggestionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            return await _context.Suggestions
                .Include(c => c.Comments)
                .ThenInclude(c => c.User)
                .Include(x => x.Comments)
                .ThenInclude(r => r.Replies)
                .ThenInclude(r => r.User)
                .ToListAsync();

        }

        public async Task<Result<Suggestion>> GetSuggestionById(int suggestionId)
        {
            var result = await _context.Suggestions
                .Where(s => s.Id == suggestionId)
                .Include(c => c.Comments)
                .ThenInclude(c => c.User)
                .Include(x => x.Comments)
                .ThenInclude(r => r.Replies)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return Result<Suggestion>.Failure(new Error(404, "Not Found", $"Suggestion with id: {suggestionId} was not found"));
            }

            return Result<Suggestion>.Success(result);
        }


        public async Task<IEnumerable<int>> CreateSuggestion(Suggestion suggestion)
        {
            await _context.AddAsync(suggestion);
            await _context.SaveChangesAsync();

            return new List<int>() { suggestion.Id };
        }

        public async Task<IEnumerable<int>> AddCommentToSuggestion(SuggestionComment comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return new List<int>() { comment.Id };
        }

        public async Task<IEnumerable<int>> AddReplyToComment(SuggestionCommentReply reply)
        {
            await _context.AddAsync(reply);
            await _context.SaveChangesAsync();

            return new List<int>() { reply.Id };
        }

        public async Task<Result<int>> DeleteSuggestion(int suggestionId)
        {
            var entityToDelete = await _context.Suggestions.Where(s => s.Id == suggestionId).Include(s => s.Comments)
                .Include(x => x.Comments)
                .ThenInclude(r => r.Replies)
                .FirstOrDefaultAsync(s => s.Id == suggestionId);


            if (entityToDelete == null)
            {
                return Result<int>.Failure(new Error(404, "Not Found", $"Suggestion with id: {suggestionId} was not found"));
            }

            _context.Suggestions.Remove(entityToDelete);
            await _context.SaveChangesAsync();

            return Result<int>.Success();
        }

        public async Task<Result<User>> GetUser(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<User>.Failure(new Error(404, "Not Found", $"User with id: {userId} was not found"));
            }

            return Result<User>.Success(user);
        }
    }
}