
using Application.Customer.Commands;
using Application.Customer.Queries;

namespace Api.Endpoints;

public class Customer : CarterModule
{
    public Customer() : base("customer")
    {
        this.WithTags("Customer");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Create", async (ISender sender, CustomerCreateCommand req, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(req, cancellationToken);

            return result.IsFailure
            ? Results.BadRequest(result.Error)
            : Results.Ok(result.Value);
        });

        app.MapPost("/GetById", async (ISender sender, CustomerGetByIdCommand req, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(req, cancellationToken);

            return result.IsFailure
          ? Results.BadRequest(result.Error)
          : Results.Ok(result.Value);
        });
    }
}
