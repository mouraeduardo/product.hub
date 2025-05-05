using Domain.Communication;
using Domain.Models.DTOs;

namespace Domain.Business;

public interface IUserBUS
{
    public void Create(CreateUserDTO dto);
    public UserResponse Login(LoginUserDTO dto);
}
