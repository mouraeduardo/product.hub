using Domain.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerBUS _customerBUS;
        public CustomerController(ICustomerBUS customerBUS)
        {
            _customerBUS = customerBUS;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            _customerBUS
            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long Id) 
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create() {
            return Ok();

        }

        [HttpPut]
        public IActionResult Update() {
            return Ok();

        }

        [HttpDelete]
        public IActionResult Delete() {
            return Ok();

        }
    }
}
