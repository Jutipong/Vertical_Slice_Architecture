
namespace Application.Abstractions.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validate
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.Run(() => _validators.Select(validators => validators.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validatorsResult => !validatorsResult.Result.IsValid)
            .SelectMany(validatorsResult => validatorsResult.Result.Errors)
            .Select(failure => new Domain.Extensions.Exceptions.ValidationError(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new Domain.Extensions.Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}

