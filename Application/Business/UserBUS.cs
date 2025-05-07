using Application.Messages;
using Application.Utils;
using Domain.Business;
using Domain.Communication;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;

namespace Application.Business;

public class UserBUS : IUserBUS 
{
    private readonly IUserRepository _userRepository;
    private readonly SecurityFunctions _securityFunctions;
    public UserBUS(IUserRepository userRepository, SecurityFunctions securityFunctions)
    {
        _userRepository = userRepository;
        _securityFunctions = securityFunctions;
    }

    public void Create(CreateUserDTO dto) 
    {
        try
        {
            if (dto.Password != dto.ConfirmedPassword)
                throw new Exception("Senhas não conferem");

            User user = new()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Salt = _securityFunctions.GenerateSalt(),
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeletionDate = null
            };

            user.Password = _securityFunctions.ComputePasswordHash(dto.Password, user.Salt);

            _userRepository.Create(user);
            _userRepository.SaveChange();
        }
        catch (Exception) {

            throw;
        }
    }

    public ApiResponse Login(LoginUserDTO dto) 
    {
        try
        {
            User user = _userRepository.GetByEmail(dto.Email).Result;

            if (user is null || user.Password != _securityFunctions.ComputePasswordHash(dto.Password, user.Salt))
                throw new Exception(ErrorMsg.ERROR001);

            string token = _securityFunctions.GenerateJwtToken(user);

            return new ApiResponse(true, "Login realizado com sucesso", token);

        }
        catch (Exception ex) 
        {
            return new ApiResponse(false, ex.Message);
        }
    }
}
