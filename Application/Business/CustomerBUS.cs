using Domain.Business;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;

namespace Application.Business;

public class CustomerBUS : ICustomerBUS
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBUS(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void Create(CreateCustomerDTO dto) 
    {
        Customer customer = new() 
        {
            Name = dto.Name,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            DeletionDate = null,
        };

        _customerRepository.Create(customer);
        _customerRepository.SaveChange();
    }

    public void Delete(long id) 
    {
        Customer customer = _customerRepository.GetByIdAsync(id).Result;

        if (customer is null)
            throw new Exception("Customer não encontrado");

        _customerRepository.Delete(customer);
        _customerRepository.SaveChange();
    }

    public IEnumerable<Customer> GetAll() 
    {
        return _customerRepository.GetAllAsync().Result;
    }

    public Customer GetById(long id) 
    {
        Customer customer = _customerRepository.GetByIdAsync(id).Result;

        if (customer is null)
            throw new Exception("Customer não encontrado");

        return customer;
    }

    public void Update(long id, CreateCustomerDTO dto) 
    {
        Customer customer = GetById(id);

        if (customer is null)
            throw new Exception("Customer não encontrado");

        customer.Name = dto.Name;
        customer.UpdateDate = DateTime.UtcNow;

        _customerRepository.Update(customer);
        _customerRepository.SaveChange();

    }
}
