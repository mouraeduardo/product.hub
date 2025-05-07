using Domain.Business;
using Domain.Communication;
using Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
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
                ApiResponse apiResponse = _productBUS.GetAll();

                if (!apiResponse.Success)
                    return BadRequest(apiResponse);

                return Ok(apiResponse);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long id) 
        {
            try 
            {
                ApiResponse apiResponse = _productBUS.GetById(id);

                if (!apiResponse.Success) 
                   return BadRequest(apiResponse);

                return Ok(apiResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDTO dto) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(x =>x.Errors)); // TODO: corrigir formatação do erro

                ApiResponse apiResponse = _productBUS.Create(dto);

                if (!apiResponse.Success)
                    return BadRequest(apiResponse);

                return Ok(apiResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] CreateProductDTO dto) 
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ApiResponse apiResponse = _productBUS.Update(id, dto);

                if (!apiResponse.Success)
                    return BadRequest(apiResponse);

                return Ok(apiResponse);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete(long id) 
        {
            try 
            {
                ApiResponse apiResponse = _productBUS.Delete(id);

                if (!apiResponse.Success)
                    return BadRequest(apiResponse);

                return Ok(apiResponse);
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
