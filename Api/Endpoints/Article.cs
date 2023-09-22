using Application.Commands.Article;

namespace Api.Endpoints;

public class Article : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/article", async (Domain.Dtos.Article.Create req, ISender sender) =>
        {
            var command = req.Adapt<CreateArticle.Command>();
            var result = await sender.Send(command);

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);

        }).WithName("Article");
    }
}
