namespace Application.Customer.Commands;
public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
{
    public CustomerCreateValidator()
    {
        RuleFor(r => r.Code).NotEmpty();
        RuleFor(r => r.Name).NotEmpty();
    }
}
