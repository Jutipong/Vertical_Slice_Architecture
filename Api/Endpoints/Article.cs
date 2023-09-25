using Application.Features.Article.Command;
using Application.Features.Article.Queries;
using Application.Features.Customer.Command;

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

        app.MapPost("", async (
            CreateArticle.Command req,
            ISender sender,
            SqlContext db,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(req, cancellationToken);
            await sender.Send(new CreateCustomer.Query { Name = "Test", Code = "code" }, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return Results.Ok(req.Id);
        });
    }
}
