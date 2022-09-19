using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractorDetailsConstController : ControllerBase
{
    private readonly DataContext _context;
    public ContractorDetailsConstController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContractorDetailsConst>>> Get()
    {
        return Ok(await _context.ContractorDetailsConsts.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContractorDetailsConst>> Get(long id)
    {
        var contractor = await _context.ContractorDetailsConsts.FindAsync(id);

        if (contractor == null)
        {
            return NotFound();
        }

        return contractor;
    }

    [HttpPost]
    public async Task<ActionResult<List<ContractorDetailsConst>>> Post(ContractorDetailsConst newContractorDetailsConst)
    {
        _context.ContractorDetailsConsts.Add(newContractorDetailsConst);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newContractorDetailsConst.Id }, newContractorDetailsConst);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutContractorDetail(long id, ContractorDetailsConst contractor)
    {
        if (id != contractor.Id)
        {
            return BadRequest();
        }

        _context.Entry(contractor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContractorDetailExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContractorDetail(long id)
    {
        var contractor = await _context.ContractorDetailsConsts.FindAsync(id);
        if (contractor == null)
        {
            return NotFound();
        }

        _context.ContractorDetailsConsts.Remove(contractor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContractorDetailExists(long id)
    {
        return _context.ContractorDetailsConsts.Any(e => e.Id == id);
    }
}