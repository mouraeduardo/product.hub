using Application.Messages;
using Domain.Business;
using Domain.Communication;
using Domain.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserBUS _userBUS;
    public UserController(IUserBUS userBUS)
    {
        _userBUS = userBUS;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody]LoginUserDTO loginUserDTO) 
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string token = _userBUS.Login(loginUserDTO); 

            return Ok(new ApiResponse(true, InfoMsg.INF005, token));
        }
        catch (Exception ex) {

            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] CreateUserDTO dto)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newUser = _userBUS.Create(dto);

            return Ok(new ApiResponse(true, InfoMsg.INF001, dto));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }
}
