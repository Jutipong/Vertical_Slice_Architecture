namespace Application.Article.Commands;

public class ArticleCreateCommand : IValidatorBase, IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
}

public class ArticleCreateValidator : AbstractValidator<ArticleCreateCommand>
{
    public ArticleCreateValidator()
    {
        RuleFor(r => r.Title).NotEmpty();
        RuleFor(r => r.Content).NotEmpty();
        RuleFor(r => r.Tags).NotEmpty();
    }
}
