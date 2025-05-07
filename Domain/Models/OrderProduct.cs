using System.Text.Json.Serialization;

namespace Domain.Models 
{
    public class OrderProduct : BaseModel
    {
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalValue { get; set; }
    }
}
