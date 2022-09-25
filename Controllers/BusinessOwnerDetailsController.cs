using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessOwnerDetailsController : ControllerBase
{
    private readonly DataContext _context;
    public BusinessOwnerDetailsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<BusinessOwnerDetails>>> Get()
    {
        try
        {
            var businessOwnerDetails = await _context.BusinessOwnerDetails.ToListAsync();
            return Ok(businessOwnerDetails);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<BusinessOwnerDetails>>> Post(BusinessOwnerDetails newBusinessOwnerDetails)
    {
        newBusinessOwnerDetails.creationDate = DateTime.Now;
        _context.BusinessOwnerDetails.Add(newBusinessOwnerDetails);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newBusinessOwnerDetails.Id }, newBusinessOwnerDetails);
    }
}