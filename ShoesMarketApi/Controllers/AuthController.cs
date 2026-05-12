using Microsoft.AspNetCore.Mvc;
using ShoesMarketApi;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public AuthController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _context.Employees
            .FirstOrDefault(x =>
                x.Login == request.Login &&
                x.Password == request.Password);

        if (user == null)
            return Unauthorized();

        return Ok(new
        {
            user.IdEmployee,
            user.LastName,
            user.FirstName,
            user.Patronymic,
            user.EmployeeRole
        });
    }
}

public class LoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}