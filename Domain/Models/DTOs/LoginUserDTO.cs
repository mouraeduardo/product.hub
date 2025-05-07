using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs; 
public class LoginUserDTO 
{
    [Required(ErrorMessage = "Campo \"Email\" é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo \"Password\" é obrigatório.")]
    public string Password { get; set; }
}
