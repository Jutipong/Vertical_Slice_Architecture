namespace Application.Customer.Command;
public static class Create
{
    public class Query : Domain.Dtos.Customer.Create, ICommandBase, IRequest<Result<Entities.Customer>> { }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<Entities.Customer>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Entities.Customer>> Handle(Query request, CancellationToken cancellationToken)
        {
            var customer = new Entities.Customer
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Age = request.Age,
                Email = request.Email
            };

            await _dbContext.AddAsync(customer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}
