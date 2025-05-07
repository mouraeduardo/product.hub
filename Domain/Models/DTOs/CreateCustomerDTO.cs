using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs
{
    public class CreateCustomerDTO 
    {
        [Required(ErrorMessage = "Campo \"Name\" é obrigatório.")]
        public string Name { get; set; }
    }
}
