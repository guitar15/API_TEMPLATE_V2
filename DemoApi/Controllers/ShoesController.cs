using DemoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ;

namespace DemoApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ShoesController : ControllerBase
{
    private readonly IShoeRepository _shoeRepository;
    private readonly IRabitMQProducer _rabitMQProducer;
    public ShoesController(IShoeRepository shoeRepository, IRabitMQProducer rabitMQProducer)
    {
        _shoeRepository = shoeRepository;
        _rabitMQProducer = rabitMQProducer;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_shoeRepository.GetShoes(ipAddress(), "GetShoes"));

    }

    [HttpGet("TestAsync")]
    public async Task<IActionResult> TestAsync()
    {
        return Ok(await Task.FromResult(_shoeRepository.GetShoes(ipAddress(), "GetShoes")));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var shoeDeleted = _shoeRepository.DeleteShoe(id);

        return shoeDeleted ? NoContent() : NotFound();
    }

    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

}
