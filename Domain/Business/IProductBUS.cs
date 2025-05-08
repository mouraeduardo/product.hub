using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Business;

public interface IProductBUS
{
    public IEnumerable<Product> GetAll();
    public Product GetById(long id);
    public Product Create(CreateProductDTO dto);
    public Product Update(long id, UpdateProductDTO dto);
    public bool Delete(long id);
    void CalculationQuantity(long id, int newQuantity);
}
