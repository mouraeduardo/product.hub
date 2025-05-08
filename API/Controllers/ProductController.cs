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
public class ProductController : Controller
{
    private readonly IProductBUS _productBUS;

    public ProductController(IProductBUS productBUS)
    {
        _productBUS = productBUS;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try 
        {
            IEnumerable<Product> productList = _productBUS.GetAll();

            if (productList.Any())
                Ok(new ApiResponse(true, InfoMsg.INF006, productList));

            return Ok(new ApiResponse(true, InfoMsg.INF004, productList));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpGet("GetById")]
    public IActionResult GetById(long id) 
    {
        try 
        {
            Product product = _productBUS.GetById(id);

            return Ok(new ApiResponse(true, InfoMsg.INF002, product));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateProductDTO dto) 
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x =>x.Errors));

            var newProduct = _productBUS.Create(dto);

            return Ok(new ApiResponse(true, InfoMsg.INF001, dto));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(long id, [FromBody] UpdateProductDTO dto) 
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updateProduct = _productBUS.Update(id, dto);

            return Ok(new ApiResponse(true, InfoMsg.INF002, dto));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(long id) 
    {
        try 
        {
            if (_productBUS.Delete(id))
                return BadRequest(ErrorMsg.ERROR010);

            return Ok(new ApiResponse(true, InfoMsg.INF003));
        }
        catch (Exception ex) 
        {
            return BadRequest(new ApiResponse(false, ex.Message));
        }
    }
}
