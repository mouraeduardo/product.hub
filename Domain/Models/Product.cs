namespace Domain.Models;

public class Product : BaseModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
}
