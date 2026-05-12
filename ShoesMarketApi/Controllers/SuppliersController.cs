using Microsoft.AspNetCore.Mvc;
using ShoesMarketApi;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public SuppliersController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Suppliers.ToList());
    }
}