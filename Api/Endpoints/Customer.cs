
using Application.Features.Customer.Command;
using Databases;

namespace Api;

public class Customer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customer", async (
            SqlContext db,
            CreateCustomer.Query req,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(req, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            // return result.IsFailure
            // ? Results.BadRequest(result.Error)
            // : Results.Ok(result.Value);
        });
    }
}
