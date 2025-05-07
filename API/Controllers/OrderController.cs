using Application.Business;
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
    public class OrderController : Controller
    {
        private readonly IOrderBUS _orderBUS;

        public OrderController(IOrderBUS orderBUS)
        {
            _orderBUS = orderBUS;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            IEnumerable<Order> orders = _orderBUS.GetAll();
            return Ok(orders);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long id) 
        {
            Order order = _orderBUS.GetById(id);
            return Ok();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateOrderDTO dto) 
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _orderBUS.Create(dto);
                return Ok();
            }
            catch (Exception) {

                throw;
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(long id, [FromBody] UpdateOrderDTO dto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _orderBUS.Update(id, dto);
            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id) 
        {
            _orderBUS.Delete(id);

            return Ok();
        }
    }
}
