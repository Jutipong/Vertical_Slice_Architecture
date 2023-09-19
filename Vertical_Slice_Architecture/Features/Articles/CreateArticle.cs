using Vertical_Slice_Architecture.Shared;
using static Vertical_Slice_Architecture.Features.Articles.CreateArticle;

namespace Vertical_Slice_Architecture.Features.Articles;

public static class CreateArticle
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Content).NotEmpty();
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
                return Result.Failure<Guid>(new Error("CreateArticle.Validation", validation.ToString()));
            }

            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                CreateOnUtc = DateTime.UtcNow
            };

            _dbContext.Add(article);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(article.Id);
        }
    }
}

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles", async (Command command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });
    }
}
