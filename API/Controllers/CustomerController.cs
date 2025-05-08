using Application.Messages;
using Domain.Business;
using Domain.Communication;
using Domain.Models;
using Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        try
        {
            IEnumerable<Customer> customerList = _customerBUS.GetAll();

            if(!customerList.Any())
                Ok(new ApiResponse(true, InfoMsg.INF006, customerList));

            return Ok(new ApiResponse(true, InfoMsg.INF004, customerList));
        }
        catch (Exception ex) {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpGet("GetById")]
    public IActionResult GetById(long Id) 
    {
        try
        {
            Customer customer = _customerBUS.GetById(Id);

            if (customer == null)
                return BadRequest(ErrorMsg.ERROR007);

            return Ok(new ApiResponse(true, InfoMsg.INF004, customer));
        }
        catch (Exception ex) {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }
       
    [HttpPost]
    public IActionResult Create([FromBody] CreateCustomerDTO dto) 
    {
        try 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = _customerBUS.Create(dto);

            return Ok(new ApiResponse(true, InfoMsg.INF001, customer));
        }
        catch (Exception ex) {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(long id, [FromBody] CreateCustomerDTO dto) 
    {
        try 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer =  _customerBUS.Update(id, dto);

            return Ok(new ApiResponse(true, InfoMsg.INF002, customer));
        }
        catch (Exception ex) {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(long id) 
    {
        try 
        {
            _customerBUS.Delete(id);

            return Ok(new ApiResponse(true, InfoMsg.INF003));
        }
        catch (Exception ex) {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }
}
