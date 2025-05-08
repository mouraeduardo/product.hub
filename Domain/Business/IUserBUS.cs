using Domain.Models.DTOs;

namespace Domain.Business;

public interface IUserBUS
{
    public CreateUserDTO Create(CreateUserDTO dto);
    public string Login(LoginUserDTO dto);
}
