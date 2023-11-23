using Application.Article.Commands;
using Application.Article.Queries;
using Domain;

namespace Api.Endpoints;

public class Article : CarterModule
{
    public Article() : base("article")
    {
        WithTags("Article");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new ArticleGetByIdQueries { Id = id });

            return result.IsFailure
           ? Results.NotFound(result.Error)
           : Results.Ok(result.Value);
        });

        app.MapPost("", async (ISender sender, ArticleCreateCommand req, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(req, cancellationToken);

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });
    }
}
