using Application.Features.Article.Command;
using Application.Features.Article.Queries;

namespace Api.Endpoints;

public class Article : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/article{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArticle.Query { Id = id });

            return result.IsFailure
           ? Results.NotFound(result.Error)
           : Results.Ok(result.Value);
        });

        app.MapPost("/article", async (CreateArticle.Command req, ISender sender) =>
        {
            var result = await sender.Send(req.Adapt<CreateArticle.Command>());

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });

    }
}
