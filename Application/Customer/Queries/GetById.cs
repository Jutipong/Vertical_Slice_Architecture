namespace Application.Customer.Queries;
public static class GetById
{
    public class CustomerGetByIdCommand : Domain.Dtos.Customer.GetById, IValidatorBase, IRequest<Result<List<Entities.Customer>>> { }

    internal sealed class Handler : IRequestHandler<CustomerGetByIdCommand, Result<List<Entities.Customer>>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<Entities.Customer>>> Handle(CustomerGetByIdCommand req, CancellationToken cancellationToken)
        {
            var customers = await _dbContext.Customer
                .Where(r => !req.Id.HasValue || r.Id == req.Id)
                .ToListAsync(cancellationToken);

            return customers is null
                ? Result.Failure<List<Entities.Customer>>(new Error("GetArticle.Null", "data not found"))
                : customers;
        }
    }
}
