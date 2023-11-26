namespace Application.Customer.Queries;

public class CustomerGetByIdQueries : IRequest<Result<List<Entities.Customer>>>
{
    public Guid? Id { get; set; }
}

public class CustomerGetByIdValidator { }