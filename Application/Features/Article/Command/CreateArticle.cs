namespace Application.Features.Article.Command;

public static class CreateArticle
{
    public class Command : ICommandBase, IRequest
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
            RuleFor(r => r.Tags).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var article = new Entities.Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                CreateOnUtc = DateTime.UtcNow,
                PublishedOnUtc = DateTime.UtcNow
            };

            await _dbContext.AddAsync(article);

            // await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}