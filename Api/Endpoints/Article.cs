using Application.Features.Article.Command;
using Application.Features.Article.Queries;

namespace Api.Endpoints;

public class Article : CarterModule
{
    public Article() : base("article")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArticle.Query { Id = id });

            return result.IsFailure
           ? Results.NotFound(result.Error)
           : Results.Ok(result.Value);
        });

        app.MapPost("", async (ISender sender, CreateArticle.Command req, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(req, cancellationToken);

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });
    }
}
