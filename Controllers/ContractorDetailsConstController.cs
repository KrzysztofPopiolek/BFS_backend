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

    [HttpGet("{displayName}")]
    public async Task<ActionResult<ContractorDetailsConst>> Get(string displayName)
    {
        // var sortedData = await _context.ContractorDetailsConsts.OrderBy(x => x.DisplayName).ToListAsync();
        try
        {
            var singleContractorDetails = await _context.ContractorDetailsConsts.FirstAsync(x => x.DisplayName == displayName);
            if (singleContractorDetails == null)
            {
                return BadRequest("Details not found");
            }
            return Ok(singleContractorDetails);
        }
        catch
        {
            return NotFound("Details not found");
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<ContractorDetailsConst>>> Post(ContractorDetailsConst newContractorDetailsConst)
    {
        _context.ContractorDetailsConsts.Add(newContractorDetailsConst);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newContractorDetailsConst.Id }, newContractorDetailsConst);
    }
}