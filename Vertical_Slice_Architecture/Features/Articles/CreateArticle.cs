using Vertical_Slice_Architecture.Abstractions.Messaging;
using Vertical_Slice_Architecture.Domains;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Articles;

public static class CreateArticle
{
    //public class Command : IRequest<Result<Guid>>
    //{
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;
    //    public string Tags { get; set; } = string.Empty;
    //}

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Content).NotEmpty();
        }
    }

    public record Command(string Title, string Content, string Tags) : ICommandBase, IRequest<Result<Guid>>;

    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                CreateOnUtc = DateTime.UtcNow,
                PublishedOnUtc = DateTime.UtcNow
            };

            _dbContext.Add(article);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return article.Id;
        }
    }
}

public class CreateArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles", async (CreateArticleRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateArticle.Command>();
            var result = await sender.Send(command);

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });
    }
}