using Vertical_Slice_Architecture.Domains;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Articles;

public static class GetArticle
{
    public class Query : IRequest<Result<ArticleResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<ArticleResponse>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ArticleResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var articleResponse = await _dbContext.Article
                .ProjectToType<ArticleResponse>()
                .FirstOrDefaultAsync(article => article.Id == request.Id, cancellationToken);

            return articleResponse is null
                ? Result.Failure<ArticleResponse>(new Error("GetArticle.Null", "data not found"))
                : articleResponse;
        }
    }
}

public class GetArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/article/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArticle.Query { Id = id }); ;

            return result.IsFailure
            ? Results.NotFound(result.Error)
            : Results.Ok(result.Value);
        });
    }
}
