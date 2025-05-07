using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs;
public class CreateProductDTO 
{
    [Required(ErrorMessage = "Campo \"Name\" é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Campo \"Description\" é obrigatório.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Campo \"Price\" é obrigatório.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Campo \"StockQuantity\" é obrigatório.")]
    public int? StockQuantity { get; set; }
}
