using Vertical_Slice_Architecture.Shared;
using static Vertical_Slice_Architecture.Features.Customer.Create;

namespace Vertical_Slice_Architecture.Features.Customer;

public static class Create
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Age).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly SqlContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(SqlContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                return Result.Failure<Guid>(new Error("Create.Validation", validation.ToString()));
            }

            var customer = new Entities.Customer
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Age = request.Age,
                Email = request.Email,
                IsActive = true,
            };

            _dbContext.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}

public class CreateArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup("Customer")
            .WithTags("Customer")
            .MapPost("api/crate", async (Command request, ISender sender) =>
            {
                var result = await sender.Send(request);
                return result.IsFailure
                ? Results.BadRequest(result.Error)
                : Results.Ok(result.Value);
            });
    }
}
