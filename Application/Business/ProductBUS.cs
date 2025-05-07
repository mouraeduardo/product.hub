using Application.Messages;
using Domain.Business;
using Domain.Communication;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;

namespace Application.Business;

public class ProductBUS : IProductBUS 
{
    private readonly IProductRepository _productRepositoy;

    public ProductBUS(IProductRepository productRepository)
    {
        _productRepositoy = productRepository;
    }

    public ApiResponse GetAll() 
    { 
        try
        {
            IEnumerable<Product> productList = _productRepositoy.GetAllAsync().Result;

            return new ApiResponse(true, InfoMsg.INF004, productList);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public ApiResponse GetById(long id)
    {
        try
        {
            Product product = _productRepositoy.GetByIdAsync(id).Result;

            return new ApiResponse(true, InfoMsg.INF004, product);

        }
        catch (Exception) 
        {
            throw;
        }
    }

    public ApiResponse Create(CreateProductDTO dto) 
    {
        try 
        {
            Product product = new() 
            {
                Code = $"PRD-{Guid.NewGuid().ToString("N").Substring(0, 5).ToUpper()}",
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeletionDate = null
            };

            _productRepositoy.Create(product);
            _productRepositoy.SaveChange();

            return new ApiResponse(true, InfoMsg.INF001);
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public ApiResponse Update(long id, CreateProductDTO dto)
    {
        try 
        {
            Product product = _productRepositoy.GetByIdAsync(id).Result;

            if (product is null) 
                throw new Exception(ErrorMsg.ERROR004); 

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.UpdateDate = DateTime.UtcNow;
            product.DeletionDate = null;

            _productRepositoy.Update(product);
            _productRepositoy.SaveChange();

            return new ApiResponse(true, InfoMsg.INF002);
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public ApiResponse Delete(long id)
    {
        try 
        {
            Product product = _productRepositoy.GetByIdAsync(id).Result;

            if (product is null)
                throw new Exception(ErrorMsg.ERROR004);

            product.DeletionDate = DateTime.UtcNow;
            _productRepositoy.Update(product);
            _productRepositoy.SaveChange();

            return new ApiResponse(true, InfoMsg.INF003);
        }
        catch (Exception) 
        {
            throw;
        }
    }
}
