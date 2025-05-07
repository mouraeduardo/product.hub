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

    public OrderBUS(IOrderRepository orderRepository, IProductBUS productBUS)
    {
        _orderRepository = orderRepository;
        _productBUS = productBUS;
    }

    // TODO: Refatorar esse método
    public void Create(CreateOrderDTO dto) 
    {
        try 
        {
            Order order = new() {
                CustomerId = dto.CustomerId,
                Observations = dto.Observations,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeletionDate = null,
                OrderProductList = new List<OrderProduct>()
            };

            foreach (var product in dto.ProductList)
            {
                Product productAux = (Product)_productBUS.GetById(product.ProductId).Data;

                if (productAux == null)
                    throw new Exception($"Produto {product.ProductId} não encontrado."); // TODO: mandar essa retorno para arquivo de msg

                if (productAux.StockQuantity < product.Quantity)
                    throw new Exception($"Quantidade de {productAux.Name} é menor do que a quantidade inserida"); // TODO: mandar essa retorno para arquivo de msg

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

                CreateProductDTO createProductDTO = new CreateProductDTO() 
                {
                    Name = productAux.Name,
                    Description = productAux.Description,
                    Price = productAux.Price,
                    StockQuantity = productAux.StockQuantity - product.Quantity
                };

                _productBUS.Update(product.ProductId, createProductDTO);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChange();

        }
        catch (Exception) {

            throw;
        }
    }

    public void Delete(long id) 
    {
        try 
        {
            Order order = _orderRepository.GetByIdAsync(id).Result;

            if (order is null)
                throw new Exception(ErrorMsg.ERROR002);

            order.DeletionDate = DateTime.UtcNow;

            foreach(var orderProduct in order.OrderProductList) 
            {
                orderProduct.DeletionDate = DateTime.UtcNow;
            }

            _orderRepository.Update(order);
            _orderRepository.SaveChange();
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
            return _orderRepository.GetWithOrderProductsAsync().Result;
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
            return _orderRepository.GetWithOrderProductsByIdAsync(id).Result;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public void Update(long id, UpdateOrderDTO dto) 
    {
        try 
        {
            Order order = GetById(id);

            if (order is null)
                throw new Exception("Pedido não encontrado"); // TODO: adicionar retorno no arquivo de msg de erro

            foreach (var productDto in dto.ProductList) 
            {
                var productAux = (Product)_productBUS.GetById(productDto.ProductId).Data;

                if (productAux == null) 
                    throw new Exception($"Produto {productDto.ProductId} não encontrado.");

                var existingProduct = order.OrderProductList.FirstOrDefault(p => p.ProductId == productDto.ProductId);

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

                    order.OrderProductList.Add(newProduct);
                }
                else if (existingProduct != null) 
                {
                    existingProduct.Quantity = productDto.Quantity;
                    existingProduct.TotalValue = productAux.Price * productDto.Quantity;
                    existingProduct.UpdateDate = DateTime.UtcNow;

                    if (productDto.IsDeleted)
                        existingProduct.DeletionDate = DateTime.UtcNow;
                }
            }

            _orderRepository.Update(order);
            _orderRepository.SaveChange();
        }
        catch (Exception) 
        {

            throw;
        }
    }
}
