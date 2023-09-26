using Application.Customer.Command;

namespace Api;

public class Customer : CarterModule
{
    public Customer() : base("customer")
    {
        this.WithTags("Customer");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (CreateCustomer.Query req, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(req, cancellationToken);

            return result.IsFailure
             ? Results.BadRequest(result.Error)
             : Results.Ok(result.Value);
        });
    }
}
