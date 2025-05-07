using Application.Messages;
using Domain.Business;
using Domain.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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

                var result = _userBUS.Login(loginUserDTO); // TODO: criar um model de reponse para retornar erros ou sucess

                if(!result.Success) return BadRequest(result.Message);

                return Ok(result);
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateUserDTO dto)
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _userBUS.Create(dto); // TODO: criar um model de reponse para retornar erros ou sucess

                return Ok(InfoMsg.INF001);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
