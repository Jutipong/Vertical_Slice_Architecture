namespace Application.Article.Commands;

public class ArticleCreateValidator : AbstractValidator<ArticleCreateCommand>
{
    public ArticleCreateValidator()
    {
        RuleFor(r => r.Title).NotEmpty();
        RuleFor(r => r.Content).NotEmpty();
        RuleFor(r => r.Tags).NotEmpty();
    }
}
