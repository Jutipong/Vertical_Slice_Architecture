namespace Application.Customer.Commands;

public class CustomerCreateCommand : IValidatorBase, IRequest<Result<Guid>>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 99;
    public string Email { get; set; } = string.Empty;
}

public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
{
    public CustomerCreateValidator()
    {
        RuleFor(r => r.Code).NotEmpty();
        RuleFor(r => r.Name).NotEmpty();
    }
}
