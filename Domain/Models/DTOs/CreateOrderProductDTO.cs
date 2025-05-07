using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs; 
public class CreateOrderProductDTO 
{
    [Required(ErrorMessage = "Campo \"ProductId\" é obrigatório.")]
    public long ProductId { get; set; }

    [Required(ErrorMessage = "Campo \"Quantity\" é obrigatório.")]
    public int Quantity { get; set; }
}
