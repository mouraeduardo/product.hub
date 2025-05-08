using Domain.Models.DTOs;
using Domain.Models;

namespace Domain.Business;

public interface ICustomerBUS
{
    public IEnumerable<Customer> GetAll();
    public Customer GetById(long id);
    public Customer Create(CreateCustomerDTO dto);
    public Customer Update(long id, CreateCustomerDTO dto);
    public bool Delete(long id);
}
