using FluentValidation;
using WorldRank.Application.Commands;
namespace WorldRank.Application.Validation;
public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}