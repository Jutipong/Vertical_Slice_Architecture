namespace Application.Article.Commands;

public static class Create
{
    public class ArticleCreateCommand : Domain.Dtos.Article.Create, ICommandBase, IRequest<Result<Guid>> { }

    public class Validator : AbstractValidator<ArticleCreateCommand>
    {
        public Validator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Content).NotEmpty();
            RuleFor(r => r.Tags).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<ArticleCreateCommand, Result<Guid>>
    {
        private readonly SqlContext _sqlContext;

        public Handler(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<Result<Guid>> Handle(ArticleCreateCommand request, CancellationToken cancellationToken)
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

            await _sqlContext.AddAsync(article, cancellationToken);

            await _sqlContext.SaveChangesAsync(cancellationToken);

            return article.Id;
        }
    }
}