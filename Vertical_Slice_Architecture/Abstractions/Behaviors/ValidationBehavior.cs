using Vertical_Slice_Architecture.Abstractions.Messaging;
using Vertical_Slice_Architecture.Exceptions;

namespace Vertical_Slice_Architecture.Abstractions.Behaviors;

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

        var errors = _validators
            .Select(validators => validators.Validate(context))
            .Where(validatorsResult => !validatorsResult.IsValid)
            .SelectMany(validatorsResult => validatorsResult.Errors)
            .Select(failure => new ValidationError(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}
