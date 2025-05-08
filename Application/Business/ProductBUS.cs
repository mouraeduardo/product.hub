using Application.Messages;
using Domain.Business;
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

    public IEnumerable<Product> GetAll() 
    { 
        try
        {
            IEnumerable<Product> productList = _productRepositoy.GetAllAsync().Result;
            
            if (productList == null)
                throw new Exception(ErrorMsg.ERROR004);

            return productList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Product GetById(long id)
    {
        try
        {
            Product product = _productRepositoy.GetByIdAsync(id).Result;

            if (product == null)
                throw new Exception(ErrorMsg.ERROR004);

            return product;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public Product Create(CreateProductDTO dto) 
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

            return product;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public Product Update(long id, UpdateProductDTO dto)
    {
        try 
        {
            Product product = GetById(id);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.UpdateDate = DateTime.UtcNow;
            product.DeletionDate = null;

            _productRepositoy.Update(product);
            _productRepositoy.SaveChange();

            return product;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public bool Delete(long id)
    {
        try 
        {
            Product product = GetById(id);

            product.DeletionDate = DateTime.UtcNow;
            _productRepositoy.Update(product);
            _productRepositoy.SaveChange();

            return true;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public void CalculationQuantity(long id, int newQuantity)
    {
        try 
        {
            Product product = GetById(id);

            product.StockQuantity -= newQuantity;

            if (product.StockQuantity < 0)
                throw new Exception(string.Format(ErrorMsg.ERROR007, product.Name));

            _productRepositoy.Update(product);
        }
        catch (Exception) 
        {
            throw;
        }
    }
}
