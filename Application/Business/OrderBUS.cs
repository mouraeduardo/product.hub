using Application.Messages;
using Domain.Business;
using Domain.Models.DTOs;
using Domain.Models;
using Domain.Repositories;

namespace Application.Business;

public class OrderBUS : IOrderBUS
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductBUS _productBUS;
    private readonly ICustomerBUS _customerBUS;


    public OrderBUS(IOrderRepository orderRepository, IProductBUS productBUS, ICustomerBUS customerBUS)
    {
        _orderRepository = orderRepository;
        _productBUS = productBUS;
        _customerBUS = customerBUS;
    }

    // TODO: Refatorar esse método
    // TODO: por dentro de uma transaction
    public Order Create(CreateOrderDTO dto) 
    {
        try 
        {
            Customer customer = _customerBUS.GetById(dto.CustomerId);

            if (customer == null)
                throw new Exception(ErrorMsg.ERROR007);
            
            Order order = new() {
                CustomerId = dto.CustomerId,
                Observations = dto.Observations,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeletionDate = null,
                OrderProductList = new List<OrderProduct>()
            };

            _orderRepository.BeginTransaction();

            foreach (var product in dto.ProductList)
            {
                Product productAux = (Product)_productBUS.GetById(product.ProductId);

                if (productAux is null)
                    throw new Exception(string.Format(ErrorMsg.ERROR006, product.ProductId));

                if (productAux.StockQuantity < product.Quantity)
                    throw new Exception(string.Format(ErrorMsg.ERROR008, productAux.Name));

                OrderProduct orderProduct = new OrderProduct() 
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    DeletionDate = null,
                    TotalValue = productAux.Price * product.Quantity,
                    Product = productAux,
                    UnitPrice = productAux.Price
                };

                order.Total += orderProduct.TotalValue;

                order.OrderProductList.Add(orderProduct);

                _productBUS.CalculationQuantity(productAux.Id, product.Quantity);

            }

            _orderRepository.Create(order);
            _orderRepository.SaveChange();

            _orderRepository.Commit();

            return order;
        }
        catch (Exception) 
        {
            _orderRepository.Rollback();
            throw;
        }
    }

    public bool Delete(long id) 
    {
        try 
        {
            Order order = _orderRepository.GetWithOrderProductsByIdAsync(id).Result;

            if (order is null)
                throw new Exception(ErrorMsg.ERROR002);

            order.DeletionDate = DateTime.UtcNow;

            foreach(var orderProduct in order.OrderProductList) 
            {
                orderProduct.DeletionDate = DateTime.UtcNow;
            }

            _orderRepository.Update(order);
            _orderRepository.SaveChange();

            return true;
        }
        catch (Exception) 
    {
            throw;
        }
    }

    public IEnumerable<Order> GetAll() 
    {
        try 
        {
            IEnumerable<Order> orderList = _orderRepository.GetWithOrderProductsAsync().Result;

            if(orderList == null)
                throw new Exception(ErrorMsg.ERROR003);

            return orderList;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public Order GetById(long id) 
    {
        try 
        {
            Order order = _orderRepository.GetWithOrderProductsByIdAsync(id).Result;

            if (order is null)
                throw new Exception(ErrorMsg.ERROR003);

            return order;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public Order Update(long id, UpdateOrderDTO dto) 
    {
        try 
        {
            Order order = GetById(id);
            Customer customer = _customerBUS.GetById(dto.CustomerId);

            if (customer == null)
                throw new Exception(ErrorMsg.ERROR007);

            _orderRepository.BeginTransaction();

            order.CustomerId = customer.Id;
            order.UpdateDate = DateTime.UtcNow;

            foreach (var productDto in dto.ProductList) 
            {
                Product productAux = _productBUS.GetById(productDto.ProductId);
                OrderProduct existingProduct = order.OrderProductList.FirstOrDefault(p => p.ProductId == productDto.ProductId);

                if (existingProduct == null) 
                {
                    OrderProduct newProduct = new OrderProduct 
                    {
                        OrderId = order.Id,
                        ProductId = productDto.ProductId,
                        Quantity = productDto.Quantity,
                        TotalValue = productAux.Price * productDto.Quantity,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };

                    order.Total += newProduct.TotalValue;
                    order.OrderProductList.Add(newProduct);
                }
                else if (existingProduct != null) 
                {
                    order.Total -= existingProduct.TotalValue;

                    _productBUS.CalculationQuantity(existingProduct.Id, productDto.Quantity - existingProduct.Quantity );

                    existingProduct.Quantity = productDto.Quantity;
                    existingProduct.TotalValue = productAux.Price * productDto.Quantity;
                    existingProduct.UpdateDate = DateTime.UtcNow;

                    if (productDto.IsDeleted)
                        existingProduct.DeletionDate = DateTime.UtcNow;

                    order.Total += existingProduct.TotalValue;
                }
            }

            _orderRepository.Update(order);

            _orderRepository.Commit();
            _orderRepository.SaveChange();

            return order;
        }
        catch (Exception) 
        {
            _orderRepository.Rollback();
            throw;
        }
    }
}
