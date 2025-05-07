using Domain.Models.DTOs;
using Domain.Models;

namespace Domain.Business;

public interface IOrderBUS
{
    public IEnumerable<Order> GetAll();
    public Order GetById(long id);
    public void Create(CreateOrderDTO dto);
    public void Update(long id, UpdateOrderDTO dto);
    public void Delete(long id);
}
 