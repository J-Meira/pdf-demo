public class Product
{
    public string Id { get; set; }
    public string Description { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public List<string> Similar { get; set; }
}

// Now you can use the htmlContent to generate the PDF as shown in the previous example.
