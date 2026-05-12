using Microsoft.AspNetCore.Mvc;

namespace ShoesMarketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProducersController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public ProducersController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Producers.ToList());
    }
}