using Microsoft.AspNetCore.Mvc;
using COMP003.LectureActivity5.Data;
using COMP003.LectureActivity5.Models;

namespace COMP003.LectureActivity5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult<List<Product>> GetAll()
    {
        return Ok(ProductStore.Products);
    }

    [HttpGet("{id}")]
    public IActionResult<Product> GetById(int id)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = ProductStore.Products.Max(p => p.Id) + 1;
        ProductStore.Products.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Product updated)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        product.Name = updated.Name;
        product.Price = updated.Price;

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = ProductStore.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        ProductStore.Products.Remove(product);

        return NoContent();
    }

    [HttpGet("filter")]
    public ActionResult<List<Product>> Filter(decimal price)
    {
        var result = ProductStore.Products
            .Where(p => p.Price <= price)
            .ToList();

        return Ok(result);
    }

    [HttpGet("names")]
    public ActionResult<List<string>> GetNames()
    {
        var names = ProductStore.Products
            .Select(p => p.Name)
            .ToList()

        return Ok(names);
    }
}