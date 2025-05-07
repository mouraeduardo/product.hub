using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTOs; 
public class CreateUserDTO
{
    [Required(ErrorMessage = "Campo \"Name\" é obrigatório.")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Informe um email válido")]
    [Required(ErrorMessage = "Campo \"Name\" é obrigatório.")]
    public string Email { get; set; }

    [StringLength(16, MinimumLength = 8, ErrorMessage = "Senha deve conter entre 8 e 16 caracteres")]
    [Required(ErrorMessage = "Campo \"Password\" é obrigatório.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Campo \"ConfirmedPassword\" é obrigatório.")]
    public string ConfirmedPassword { get; set; }
}
