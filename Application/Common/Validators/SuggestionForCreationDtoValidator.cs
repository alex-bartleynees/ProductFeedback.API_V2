using Application.Common.Models;
using Domain.Entities;
using FluentValidation;

namespace Application.Common.Validators
{
    public class SuggestionForCreationDtoValidator : AbstractValidator<SuggestionForCreationDto>
    {
        public SuggestionForCreationDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("You must provide a title");
            RuleFor(x => x.Title).MinimumLength(3);
            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Category).MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}