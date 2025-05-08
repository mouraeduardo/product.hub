using Application.Messages;
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

    public Customer Create(CreateCustomerDTO dto) 
    {
        try 
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

            return customer;
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
            Customer customer = _customerRepository.GetByIdAsync(id).Result;

            if (customer is null)
                throw new Exception(ErrorMsg.ERROR007);

            _customerRepository.Delete(customer);
            _customerRepository.SaveChange();

             return true;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public IEnumerable<Customer> GetAll() 
    {
        try 
        {
            IEnumerable<Customer> customerList = _customerRepository.GetAllAsync().Result;

            if (customerList == null)
                throw new Exception(ErrorMsg.ERROR007);

            return customerList;
        }
        catch (Exception) {

            throw;
        }
    }

    public Customer GetById(long id) 
    {
        try 
        {
            Customer customer = _customerRepository.GetByIdAsync(id).Result;

            if (customer is null)
                throw new Exception(ErrorMsg.ERROR007);

            return customer;
        }
        catch (Exception) {

            throw;
        }
    }

    public Customer Update(long id, CreateCustomerDTO dto) 
    {
        try 
        {
            Customer customer = GetById(id);

            if (customer is null)
                throw new Exception(ErrorMsg.ERROR007);

            customer.Name = dto.Name;
            customer.UpdateDate = DateTime.UtcNow;

            _customerRepository.Update(customer);
            _customerRepository.SaveChange();

            return customer;
        }
        catch (Exception) 
        {
            throw;
        }
    }
}
