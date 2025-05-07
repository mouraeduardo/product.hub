using Domain.Business;
using Domain.Models;
using Domain.Models.DTOs;
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
            IEnumerable<Customer> customerList = _customerBUS.GetAll();

            return Ok(customerList);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long Id) 
        {
            Customer customer = _customerBUS.GetById(Id);

            if (customer == null)
                return BadRequest("Cliente não encontrado"); // TODO: Ajustar mensagem

            return Ok(customer);
        }
           
        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerDTO dto) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            _customerBUS.Create(dto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] CreateCustomerDTO dto) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            _customerBUS.Update(id, dto);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(long id) 
        {
            _customerBUS.Delete(id);

            return Ok();
        }
    }
}
