using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ACME.Models;
using ACME.DTO;
using ACME.Repository;
using ACME.Extensions;

namespace ACME.Controllers;

[Route(ApiRoute.Default)]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    public IActionResult GetAll()
    {
        var result = _repository.GetAll();
        return Ok(result);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get(int id)
    {
        var result = _repository.Get(id);
        if (result is null)
        {
            return NoContent();
        }
        return Ok(result);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Find(string description)
    {
        var result = _repository.Find(description);
        if (result.Count == 0)
        {
            return NoContent();
        }
        return Ok(result);
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add(ProductRequest request)
    {
        try
        {
            var result = _repository.Add(request);
            return Ok(result);
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
    public IActionResult Update(int id, ProductRequest request)
    {
        try
        {
            var updated = _repository.Update(id, request);
            return updated ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            return this.BadRequestMessage(ex);
        }
    }
}
