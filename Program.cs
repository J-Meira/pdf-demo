using System.Security.Cryptography;

List<Product> products = new List<Product>();

for (int i = 1; i <= 100; i++)
{
    var iS = i.ToString();
    var number = RandomNumberGenerator.GetInt32(i);
    var quant = RandomNumberGenerator.GetInt32(3) + 1;
    var similar = new List<string>();
    for (int j = 1; j <= quant; j++)
    {
        similar.Add("SIM" + j.ToString());
    }
    products.Add(

    new Product {
        Id = "PROD00"+iS,
        Description = "DESCRIPTION OF PRODUCT TO TEST PDF TAG SIZE",
        CostPrice = number * 1.25m,
        SalePrice = number * 2.5m,
        Similar = similar
    }
    );
}

var pdfGenerator = new PdfGenerator();
string htmlContent = pdfGenerator.GenerateHtmlFromProducts(products);

var count = RandomNumberGenerator.GetInt32(45).ToString();
string outputPath = $"output{count}.pdf";

pdfGenerator.GenerateSyncfusion(htmlContent, outputPath);
// Now you can use the htmlContent to generate the PDF as shown in the previous example.
