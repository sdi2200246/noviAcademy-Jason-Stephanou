using FluentValidation;
using WorldRank.Application.Commands;
namespace WorldRank.Application.Validation;
public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(x => x.currency).IsInEnum();
        RuleFor(x => x.playerId).NotEmpty();
    }
}