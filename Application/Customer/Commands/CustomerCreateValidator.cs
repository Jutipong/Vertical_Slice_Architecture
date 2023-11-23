namespace Application.Customer.Commands;

public class CustomerCreateCommand : Domain.Dtos.Customer.Create, IValidatorBase, IRequest<Result<Guid>> { }
public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
{
    public CustomerCreateValidator()
    {
        RuleFor(r => r.Code).NotEmpty();
        RuleFor(r => r.Name).NotEmpty();
    }
}
