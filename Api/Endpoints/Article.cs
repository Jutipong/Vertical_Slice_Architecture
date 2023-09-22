﻿using System.Transactions;
using Application.Features.Article.Command;
using Application.Features.Article.Queries;
using Application.Features.Customer.Command;
using Databases;

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

        app.MapPost("/article", async (
            SqlContext db,
            CreateArticle.Command req,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(req, cancellationToken);
            await sender.Send(new CreateCustomer.Query { Name = "Test", Code = "code" }, cancellationToken);
            // await sender.Send(req);

            // await sender.Send(new CreateCustomer.Query { Name = "Test", Code = "code" });

            await db.SaveChangesAsync(cancellationToken);

            // return result.IsFailure
            // ? Results.BadRequest(result.Error)
            // : Results.Ok(result.Value);
        });

    }
}
