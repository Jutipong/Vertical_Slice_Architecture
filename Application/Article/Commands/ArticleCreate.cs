namespace Application.Article.Commands;

public class ArticleCreateCommand : Domain.Dtos.Article.Create, IValidatorBase, IRequest<Result<Guid>> { }

internal sealed class Handler(SqlContext _db) : IRequestHandler<ArticleCreateCommand, Result<Guid>>
{
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

        await _db.AddAsync(article, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}