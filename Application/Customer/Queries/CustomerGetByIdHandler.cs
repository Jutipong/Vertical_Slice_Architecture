namespace Application.Customer.Queries;

internal sealed class CustomerGetByIdHandler(SqlContext _db)
: IRequestHandler<CustomerGetByIdQueries, Result<List<Entities.Customer>>>
{
    public async Task<Result<List<Entities.Customer>>> Handle(CustomerGetByIdQueries req, CancellationToken cancellationToken)
    {
        var customers = await _db.Customer
            .Where(r => !req.Id.HasValue || r.Id == req.Id)
            .ToListAsync(cancellationToken);

        return customers is null
            ? Result.Failure<List<Entities.Customer>>(new Error("GetArticle.Null", "data not found"))
            : customers;
    }
}
