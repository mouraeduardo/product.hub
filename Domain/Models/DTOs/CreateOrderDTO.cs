using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs; 
public class CreateOrderDTO 
{
    [Required(ErrorMessage = "Campo \"CustomerId\" é obrigatório.")]
    public long CustomerId { get; set; }

    [Required(ErrorMessage = "Campo \"Observations\" é obrigatório.")]
    public string Observations { get; set; }

    [Required(ErrorMessage = "Por favor, insira ao menos um produto")]
    public ICollection<CreateOrderProductDTO> ProductList { get; set; }
}
