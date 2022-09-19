using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxRateController : ControllerBase
{
    private readonly DataContext _context;
    public TaxRateController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaxRate>>> Get()
    {
        return Ok(await _context.TaxRates.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<TaxRate>>> Post(TaxRate newTaxRate)
    {
        _context.TaxRates.Add(newTaxRate);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newTaxRate.Id }, newTaxRate);
    }
}