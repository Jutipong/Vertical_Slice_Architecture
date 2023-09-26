﻿namespace Application.Features.Article.Queries;
public static class GetArticle
{
    public class Query : ICommandBase, IRequest<Result<Entities.Article>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<Entities.Article>>
    {
        private readonly SqlContext _dbContext;

        public Handler(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Entities.Article>> Handle(Query request, CancellationToken cancellationToken)
        {
            var articleResponse = await _dbContext.Article
                .FirstOrDefaultAsync(article => article.Id == request.Id, cancellationToken);

            return articleResponse is null
                ? Result.Failure<Entities.Article>(new Error("GetArticle.Null", "data not found"))
                : articleResponse;
        }
    }
}