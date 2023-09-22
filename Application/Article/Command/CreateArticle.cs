namespace Application.Article.Command;
public static class CreateArticle
{
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
            var article = new Entities.Article
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
