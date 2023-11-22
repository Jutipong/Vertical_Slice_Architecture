namespace Application.Customer.Commands;

public class CustomerCreateCommand : Domain.Dtos.Customer.Create, IValidatorBase, IRequest<Result<Guid>> { }

internal sealed class Handler(SqlContext _db) : IRequestHandler<CustomerCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        var customer = new Entities.Customer
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Age = request.Age,
            Email = request.Email
        };

        await _db.AddAsync(customer, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}