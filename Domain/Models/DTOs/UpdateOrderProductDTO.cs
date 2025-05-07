namespace Domain.Models.DTOs;
public class UpdateOrderProductDTO 
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsDeleted { get; set; }
}
