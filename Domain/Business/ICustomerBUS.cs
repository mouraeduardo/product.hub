using Domain.Models.DTOs;
using Domain.Models;

namespace Domain.Business;

public interface ICustomerBUS
{
    public IEnumerable<Customer> GetAll();
    public Customer GetById(long id);
    public void Create(CreateCustomerDTO dto);
    public void Update(long id, CreateCustomerDTO dto);
    public void Delete(long id);
}
