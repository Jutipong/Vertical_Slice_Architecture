namespace Application.Customer.Command;
public static class CreateCustomer
{
    public class Query : ICommandBase, IRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; } = 99;
        public string Email { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Query>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(Query request, CancellationToken cancellationToken)
        {
            var article = new Entities.Customer
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Age = request.Age,
                Email = request.Email
            };

            await _dbContext.AddAsync(article);

            // await _dbContext.SaveChangesAsync(cancellationToken);

            // return article.Id;
        }
    }
}
