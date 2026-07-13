using FluentValidation;
using WorldRank.Api.Requests;
namespace WorldRank.Api.Validation;

public class AddPlayerRequestValidator : AbstractValidator<AddPlayerRequest>
{
	public AddPlayerRequestValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
		RuleFor(x => x.Score).GreaterThanOrEqualTo(0);
	}
}