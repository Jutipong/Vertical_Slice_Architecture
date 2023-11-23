namespace Application.Article.Queries;

internal sealed class ArticleGetByIdHandler(SqlContext _db)
: IRequestHandler<ArticleGetByIdQueries, Result<Entities.Article>>
{
    public async Task<Result<Entities.Article>> Handle(ArticleGetByIdQueries request, CancellationToken cancellationToken)
    {
        var articleResponse = await _db.Article
            .FirstOrDefaultAsync(article => article.Id == request.Id, cancellationToken);

        return articleResponse is null
            ? Result.Failure<Entities.Article>(new Error("GetArticle.Null", "data not found"))
            : articleResponse;
    }
}
