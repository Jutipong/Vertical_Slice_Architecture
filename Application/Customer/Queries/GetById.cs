namespace Application.Customer.Queries;
public static class GetById
{
    public class Query : Domain.Dtos.Customer.GetById, ICommandBase, IRequest<Result<List<Entities.Customer>>> { }

    internal sealed class Handler : IRequestHandler<Query, Result<List<Entities.Customer>>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<Entities.Customer>>> Handle(Query req, CancellationToken cancellationToken)
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
