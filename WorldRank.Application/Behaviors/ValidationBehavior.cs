using FluentValidation;
using MediatR;

namespace WorldRank.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        foreach (var v in _validators)
        {
            var result = await v.ValidateAsync(request, ct);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
        return await next();
    }
}   