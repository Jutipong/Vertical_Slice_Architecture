﻿using static Application.Article.Commands.Create;
using static Application.Article.Queries.GetById;

namespace Api.Endpoints;

public class Article : CarterModule
{
    public Article() : base("article")
    {
        this.WithTags("Article");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new ArticleGetByIdCommand { Id = id });

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
