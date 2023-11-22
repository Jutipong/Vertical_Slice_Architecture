namespace Application.Customer.Commands;

public static class Create
{
    public class CustomerCreateCommand : Domain.Dtos.Customer.Create, IValidatorBase, IRequest<Result<Guid>> { }

    public class Validator : AbstractValidator<CustomerCreateCommand>
    {
        public Validator()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<CustomerCreateCommand, Result<Guid>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

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

            await _dbContext.AddAsync(customer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}