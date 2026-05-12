using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesMarketApi;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public CategoriesController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.ProductCategories.ToList());
    }
}