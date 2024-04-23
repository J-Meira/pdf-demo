using System.Text;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

public class PdfGenerator
{
    public string GenerateHtmlFromProducts(List<Product> products)
    {
        var htmlBuilder = new StringBuilder();

        htmlBuilder.AppendLine("<!DOCTYPE html><html><head><mate charest=\"utf-8\"><title>Product Tags</title><style type=\"text/css\">");
        htmlBuilder.AppendLine("body{font-family:Arial,Helvetica,sans-serif}");
        htmlBuilder.AppendLine(".tags-list { width: 100%; display: flex; flex-direction: row; flex-wrap: wrap; justify-content: flex-start; align-content: flex-start;align-items: baseline; }");
        htmlBuilder.AppendLine(".product { border: 3px solid #000; padding: 5px; width: calc(25% - 16px); } .id { background-color: #000000; color: #ffffff; font-size: 19px; text-align: center; padding: 2px; }");
        htmlBuilder.AppendLine(".description { width: 100%; padding: 0 8px; padding-top: 5px; text-align: center; min-height: 38px; font-size: 12px; }");
        htmlBuilder.AppendLine(".description span{ display:block; width: calc(100% - 16px); word-break: break-all; }");
        htmlBuilder.AppendLine(".prices { display: flex; flex-direction: row; flex-wrap: nowrap; justify-content: center; align-content: center; align-items: center; margin-bottom: 5px; }");
        htmlBuilder.AppendLine(".cost { font-size: 12px; margin-right: 8px; } .sale { font-size: 20px; font-weight: 700; }");
        htmlBuilder.AppendLine(".similar { font-size: 11px; text-align: center; min-height: 30px; } .white { color:#ffffff; } .gray{ color: #dedede; }");
        htmlBuilder.AppendLine("@media print { .product { page-break-inside: avoid; } }");
        htmlBuilder.AppendLine("</style></head><body>");

        htmlBuilder.AppendLine("<div class='tags-list'>");

        foreach (var product in products)
        {
            htmlBuilder.AppendLine("<div class='product'>");
            htmlBuilder.AppendLine($"<div class='id'><span><strong>{product.Id}</strong></span></div>");
            htmlBuilder.AppendLine($"<div class='description'><span>{product.Description}</span></div>");
            htmlBuilder.AppendLine("<div class='prices'>");
            htmlBuilder.AppendLine($"<div class='cost'>R$ <span class='gray'>{product.CostPrice}</span></div>");
            htmlBuilder.AppendLine($"<div class='sale'>R$ <span class='white'>{product.SalePrice}</span></div>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("<div class='similar'>");

            foreach (var similarProduct in product.Similar)
            {
                htmlBuilder.AppendLine(similarProduct);
            }

            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("</div>");
        }

        htmlBuilder.AppendLine("</div>");
        htmlBuilder.AppendLine("</body></html>");

        return htmlBuilder.ToString();
    }

    public void GenerateSyncfusion(string htmlContent, string outputPath)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("key");
        var htmlConverter = new HtmlToPdfConverter();

        BlinkConverterSettings settings = new BlinkConverterSettings
        {
            EnableJavaScript = true,
            MediaType = MediaType.Print,

            Orientation = PdfPageOrientation.Portrait,
            PdfPageSize = PdfPageSize.A4,
            ViewPortSize = new Size(860, 0),
            // PdfHeader = new PdfPageTemplateElement

            Margin = new PdfMargins { Top = 10, Left = 10, Right = 10, Bottom = 10 }
        };
        htmlConverter.ConverterSettings = settings;

        var document = htmlConverter.Convert(htmlContent, "");

        var fileStream = new FileStream(outputPath, FileMode.CreateNew, FileAccess.ReadWrite);

        document.Save(fileStream);
        document.Close(true);
    }
}



// Now you can use the htmlContent to generate the PDF as shown in the previous example.
