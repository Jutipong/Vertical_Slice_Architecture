namespace Application.Features.Article.Queries;
public static class GetArticle
{
    public class Query : ICommandBase, IRequest<Result<object>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<object>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<object>> Handle(Query request, CancellationToken cancellationToken)
        {
            var articleResponse = await _dbContext.Article
                .FirstOrDefaultAsync(article => article.Id == request.Id, cancellationToken);

            return articleResponse is null
                ? Result.Failure<object>(new Error("GetArticle.Null", "data not found"))
                : articleResponse;
        }
    }
}
