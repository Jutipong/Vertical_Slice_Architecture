using Carter;
using Wkhtmltopdf.NetCore;
using Wkhtmltopdf.NetCore.Options;

namespace Presentation.Api.Report.Endpoints;

public class DemoReport : CarterModule
{
    public DemoReport() : base("DemoReport")
    {
        this.WithTags("DemoReport");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (IGeneratePdf _generatePdf) =>
        {
            _generatePdf.SetConvertOptions(new ConvertOptions
            {
                PageOrientation = Orientation.Portrait,
                PageSize = Size.A4,
                PageMargins = new Margins()
                {
                    Top = 10,
                    Left = 5,
                    Right = 5,
                    Bottom = 10,
                },
            });

            var mockData = new Domain.Dtos.DemoReport.Report1 { Title = "Title", Name = "Name" };
            var byteArray = await _generatePdf.GetByteArray("~/Views/DemoReport/Report1.cshtml", mockData);
            return Results.File(byteArray, "application/pdf", $"{DateTime.Now}.pdf");
        });
    }
}
