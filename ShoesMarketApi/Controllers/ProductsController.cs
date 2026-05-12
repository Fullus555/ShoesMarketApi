using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesMarketApi;
using System;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly OooObyvTimkomContext _context;

    public ProductsController(OooObyvTimkomContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string search = "", int? supplierId = null)
    {
        var query = _context.Products
            .Include(p => p.ProductCategoryFkNavigation)
            .Include(p => p.ProducerFkNavigation)
            .Include(p => p.SupplierFkNavigation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var words = search.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                query = query.Where(p =>
                    p.ProductName.ToLower().Contains(word) ||
                    p.ProductDescription.ToLower().Contains(word) ||
                    p.SupplierFkNavigation.SupplierName.ToLower().Contains(word)
                );
            }
        }

        if (supplierId != null && supplierId != 0)
        {
            query = query.Where(p => p.SupplierFk == supplierId);
        }

        return Ok(await query.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
     [FromForm] string productData,
     IFormFile? imageFile)
    {
        var product = JsonSerializer.Deserialize<Product>(productData);

        if (imageFile != null)
        {
            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(imageFile.FileName);

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                fileName);

            using var stream = new FileStream(path, FileMode.Create);

            await imageFile.CopyToAsync(stream);

            product.Photo = fileName;
        }

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
     int id,
     [FromForm] string productData,
     IFormFile? imageFile)
    {
        var updatedProduct =
            JsonSerializer.Deserialize<Product>(productData);

        var product = _context.Products
            .FirstOrDefault(x => x.IdProduct == id);

        if (product == null)
            return NotFound();

        product.ProductName = updatedProduct.ProductName;
        product.ArticleNumber = updatedProduct.ArticleNumber;
        product.Price = updatedProduct.Price;
        product.CurrentDiscount = updatedProduct.CurrentDiscount;
        product.QuantityInWarehouse = updatedProduct.QuantityInWarehouse;
        product.ProductDescription = updatedProduct.ProductDescription;
        product.SupplierFk = updatedProduct.SupplierFk;
        product.ProducerFk = updatedProduct.ProducerFk;
        product.ProductCategoryFk = updatedProduct.ProductCategoryFk;
        product.UnitMeasurement = updatedProduct.UnitMeasurement;

        if (imageFile != null)
        {
            if (!string.IsNullOrWhiteSpace(product.Photo))
            {
                var oldPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Photo);

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(imageFile.FileName);

            var newPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                fileName);

            using var stream = new FileStream(
                newPath,
                FileMode.Create);

            await imageFile.CopyToAsync(stream);

            product.Photo = fileName;
        }

        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpGet("producers")]
    public IActionResult GetProducers()
    {
        return Ok(_context.Producers.ToList());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        bool hasOrders = _context.ItemQuantities
            .Any(x => x.ArticleNumberFk == product.ArticleNumber);

        if (hasOrders)
        {
            return BadRequest(
                "Товар нельзя удалить, он присутствует в заказах");
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
