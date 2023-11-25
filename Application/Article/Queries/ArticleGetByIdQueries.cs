namespace Application.Article.Queries;

public class ArticleGetByIdQueries : Domain.Dtos.Article.GetById, IRequest<Result<Entities.Article>> { }

public class ArticleGetByIdValidator { }