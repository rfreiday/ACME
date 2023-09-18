using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ACME.Models;
using ACME.DTO;
using ACME.Repository;
using ACME.Extensions;

namespace ACME.Controllers;

[Route(ApiRoute.Default)]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;   
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerResponse>))]
    public IActionResult GetAll()
    {
        var result = _repository.GetAll();
        var response = _mapper.Map<List<CustomerResponse>>(result);
        return Ok(response);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get(int id)
    {
        var result = _repository.Get(id);
        if (result is null)
        {
            return NoContent();
        }
        var response = _mapper.Map<CustomerResponse>(result);
        return Ok(response);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetEmail(string email)
    {
        var result = _repository.GetEmail(email);
        if (result is null)
        {
            return NoContent();
        }
        var response = _mapper.Map<CustomerResponse>(result);
        return Ok(response);
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Find(CustomerFindRequest findRequest)
    {
        var result = _repository.Find(findRequest);
        var response = _mapper.Map<List<CustomerResponse>>(result);
        return Ok(response);
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add(CustomerRequest request)
    {
        try
        {
            var customer = _mapper.Map<Customer>(request);
            var id = _repository.Add(customer);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return this.BadRequestMessage(ex);
        }
    }

    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, CustomerRequest request)
    {
        try
        {
            //var customer = _mapper.Map<Customer>(request);
            var updated = _repository.Update(id, request);
            return updated ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            return this.BadRequestMessage(ex);
        }
    }

    [HttpDelete()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = _repository.Delete(id);
            return deleted ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            return this.BadRequestMessage(ex);
        }
    }
}