namespace Application.Customer.Commands;

internal sealed class CustomerCreateHandler(SqlContext _db)
: IRequestHandler<CustomerCreateCommand, Result<Guid>>
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