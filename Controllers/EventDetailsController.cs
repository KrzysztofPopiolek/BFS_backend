using BFS_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventDetailsController : ControllerBase
{
    private readonly DataContext _context;
    public EventDetailsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventDetails>>> Get()
    {
        return Ok(await _context.EventDetails.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDetails>> Get(int id)
    {
        var singleEventDetails = await _context.EventDetails.FindAsync(id);
        if (singleEventDetails == null)
        {
            return BadRequest("Details not found");
        }
        return Ok(singleEventDetails);
    }
}

