﻿namespace Application.Customer.Queries;
public class CustomerGetByIdCommand : Domain.Dtos.Customer.GetById, IRequest<Result<List<Entities.Customer>>> { }

internal sealed class Handler(SqlContext _db) : IRequestHandler<CustomerGetByIdCommand, Result<List<Entities.Customer>>>
{
    public async Task<Result<List<Entities.Customer>>> Handle(CustomerGetByIdCommand req, CancellationToken cancellationToken)
    {
        var customers = await _db.Customer
            .Where(r => !req.Id.HasValue || r.Id == req.Id)
            .ToListAsync(cancellationToken);

        return customers is null
            ? Result.Failure<List<Entities.Customer>>(new Error("GetArticle.Null", "data not found"))
            : customers;
    }
}
