using BFS_backend.Data;
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
}