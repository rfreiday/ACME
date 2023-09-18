using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ACME.Models;
using ACME.DTO;
using ACME.Repository;

namespace ACME.Controllers;

[Route(ApiRoute.Default)]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderController(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetSingle(int id)
    {
        var order = _repository.Get(id);
        if (order is null)
        {
            return NoContent();
        }
        var result = _mapper.Map<OrderResponse>(order);
        result.Items.AddRange(_mapper.Map<IEnumerable<OrderItemResponse>>(order.OrderItems));
        result.Payments.AddRange(_mapper.Map<IEnumerable<OrderPaymentResponse>>(order.OrderPayments));

        return Ok(result);
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add(OrderRequest orderRequest)
    {
        try
        {
            var id = _repository.Add(orderRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddItem(int orderId, OrderItemRequest orderItemRequest)
    {
        try
        {
            _repository.AddOrderItem(orderId, orderItemRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteItem(int orderId, OrderItemRequest orderItemRequest)
    {
        try
        {
            _repository.DeleteOrderItem(orderId, orderItemRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddPayment(int orderId, OrderPaymentRequest orderPaymentRequest)
    {
        try
        {
            _repository.AddOrderPayment(orderId, orderPaymentRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeletePayment(int orderId, OrderPaymentRequest orderPaymentRequest)
    {
        try
        {
            _repository.DeleteOrderPayment(orderId, orderPaymentRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult ShipOrder(int orderId)
    {
        try
        {
            _repository.ShipOrderAync(orderId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
