namespace Domain.Models.DTOs; 
public class UpdateOrderDTO 
{
    public long CustomerId { get; set; }
    public string Observations { get; set; }
    public ICollection<UpdateOrderProductDTO> ProductList { get; set; }
}
