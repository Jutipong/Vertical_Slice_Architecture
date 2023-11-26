namespace Application.Article.Queries;

public class ArticleGetByIdQueries : IRequest<Result<Entities.Article>>
{
    public Guid Id { get; set; }
}

public class ArticleGetByIdValidator { }