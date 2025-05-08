using Domain.Models.DTOs;
using Domain.Models;

namespace Domain.Business;

public interface IOrderBUS
{
    public IEnumerable<Order> GetAll();
    public Order GetById(long id);
    public Order Create(CreateOrderDTO dto);
    public Order Update(long id, UpdateOrderDTO dto);
    public bool Delete(long id);
}
 