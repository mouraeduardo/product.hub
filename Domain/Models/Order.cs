namespace Domain.Models;

public class Order : BaseModel
{
    public long CustomerId { get; set; }
    public double Total { get; set; }
    public int Status { get; set; }
    public string Observations{ get; set; }
    public ICollection<OrderProduct> OrderProductList { get; set; }
}
