using Application.Messages;
using Domain.Business;
using Domain.Communication;
using Domain.Models;
using Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
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
            try 
            {
                IEnumerable<Order> orderList = _orderBUS.GetAll();

                if (orderList.Any())
                    Ok(new ApiResponse(true, InfoMsg.INF006, orderList));

                return Ok(new ApiResponse(true, InfoMsg.INF004, orderList));
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(false, ex.Message));
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long id) 
        {
            try
            {
                Order order = _orderBUS.GetById(id);
                return Ok(new ApiResponse(true, InfoMsg.INF004, order));
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(false, ex.Message));
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateOrderDTO dto) 
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                Order order = _orderBUS.Create(dto);

                return Ok(new ApiResponse(true, InfoMsg.INF001, order));
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(false, ex.Message));
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(long id, [FromBody] UpdateOrderDTO dto) 
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                Order order = _orderBUS.Update(id, dto);
                return Ok(new ApiResponse(true, InfoMsg.INF002, order));

            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(false, ex.Message));
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id) 
        {
            try 
            {
                bool sucess = _orderBUS.Delete(id);

                return Ok(new ApiResponse(sucess, InfoMsg.INF003));
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(false, ex.Message));
            }
        }
    }
}
