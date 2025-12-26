using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
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

        public async Task<IEnumerable<SuggestionDto>> GetSuggestions()
        {
            return await _context.Suggestions
                .AsNoTracking()
                .GroupJoin(
                    _context.SuggestionsComment,
                    suggestion => suggestion.Id,
                    comment => comment.SuggestionId,
                    (suggestion, comments) => new SuggestionDto(
                        suggestion,
                        comments.Count()
                    ))
                .ToListAsync();

        }

        public async Task<Result<Suggestion>> GetSuggestionById(int suggestionId)
        {
            var result = await _context.Suggestions
                .AsNoTracking()
                .Where(s => s.Id == suggestionId)
                .Select(s => new Suggestion
                {
                    Id = s.Id,
                    Title = s.Title,
                    Upvotes = s.Upvotes,
                    Category = s.Category,
                    Status = s.Status,
                    Description = s.Description,
                    Comments = s.Comments.Select(c => new SuggestionComment(c.Content)
                    {
                        Id = c.Id,
                        User = c.User,
                        SuggestionId = c.SuggestionId,
                        Replies = c.Replies.Select(r => new SuggestionCommentReply(r.Content, r.ReplyingTo)
                        {
                            Id = r.Id,
                            User = r.User,
                            SuggestionCommentId = r.SuggestionCommentId
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return result == null
                ? Result<Suggestion>.Failure(new Error(404, "Not Found", $"Suggestion with id: {suggestionId} was not found"))
                : Result<Suggestion>.Success(result);
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

        public async Task<Result<User>> GetUser(Guid userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<User>.Failure(new Error(404, "Not Found", $"User with id: {userId} was not found"));
            }

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<User>.Failure(new Error(404, "Not Found", $"User with email: {email} was not found"));
            }

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Result<User>.Success(user);
        }

        public async Task<Result<Suggestion>> UpdateSuggestion(int suggestionId, Suggestion suggestion)
        {
            var entityToUpdate = await _context.Suggestions
                .Where(s => s.Id == suggestionId)
                .FirstOrDefaultAsync();

            if (entityToUpdate != null)
            {
                entityToUpdate.Title = suggestion.Title;
                entityToUpdate.Upvotes = suggestion.Upvotes;
                entityToUpdate.Category = suggestion.Category;
                entityToUpdate.Status = suggestion.Status;
                entityToUpdate.Description = suggestion.Description;

                await _context.SaveChangesAsync();

                return Result<Suggestion>.Success(entityToUpdate);
            }

            return Result<Suggestion>.Failure(new Error(404, "Not Found", $"Suggestion with id: {suggestion.Id} was not found"));
        }
    }
}