using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesMarketApi;

namespace ShoesMarketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public OrdersController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<List<OrderStatus>>> GetStatuses()
    {
        return await _context.OrderStatuses.ToListAsync();
    }

    [HttpGet("pickuppoints")]
    public async Task<ActionResult<List<PickUpPoint>>> GetPickUpPoints()
    {
        return await _context.PickUpPoints.ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<List<object>>> GetOrders()
    {
        var orders = await _context.Orders

            .Include(x => x.OrderStatusFkNavigation)

            .Include(x => x.PickUpPointFkNavigation)

            .Include(x => x.ItemQuantities)

            .Select(x => new
            {
                x.OrderNumber,
                x.OrderDate,
                x.DeliveryDate,
                x.PickUpPointFk,
                x.FullNameOfEmployeeFk,
                x.ReceiptCode,
                x.OrderStatusFk,

                StatusName =
                    x.OrderStatusFkNavigation.Status,

                PickUpPointName =
                    x.PickUpPointFkNavigation.City
                    + ", "
                    + x.PickUpPointFkNavigation.StreetAndHouse,

                ArticleNumber =
                    x.ItemQuantities
                     .FirstOrDefault()
                     .ArticleNumberFk
            })

            .ToListAsync();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(
        int id,
        [FromBody] Order order)
    {
        var current = await _context.Orders
            .FirstOrDefaultAsync(x => x.OrderNumber == id);

        if (current == null)
            return NotFound();

        current.OrderDate = order.OrderDate;
        current.DeliveryDate = order.DeliveryDate;
        current.PickUpPointFk = order.PickUpPointFk;
        current.OrderStatusFk = order.OrderStatusFk;
        current.ReceiptCode = order.ReceiptCode;
        current.FullNameOfEmployeeFk = order.FullNameOfEmployeeFk;

        await _context.SaveChangesAsync();

        return Ok(current);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.OrderNumber == id);

        if (order == null)
            return NotFound();

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return Ok();
    }
}