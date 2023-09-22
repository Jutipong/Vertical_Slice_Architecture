namespace Application.Extensions.Swagger;

internal static partial class ApplicationBuilder
{
    public static IApplicationBuilder UseSwaggerEndpoints(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            //c.RoutePrefix = string.Empty;
        });

        return app;
    }
}
