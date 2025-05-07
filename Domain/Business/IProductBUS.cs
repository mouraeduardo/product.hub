using Domain.Communication;
using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Business;

public interface IProductBUS
{
    public ApiResponse GetAll();
    public ApiResponse GetById(long id);
    public ApiResponse Create(CreateProductDTO dto);
    public ApiResponse Update(long id, CreateProductDTO dto);
    public ApiResponse Delete(long id);
}
