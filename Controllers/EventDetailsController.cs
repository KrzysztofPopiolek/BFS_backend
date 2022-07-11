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
    public async Task<ActionResult<EventDetails>> Get(long id)
    {
        var singleEventDetails = await _context.EventDetails.FindAsync(id);
        if (singleEventDetails == null)
        {
            return BadRequest("Details not found");
        }
        return Ok(singleEventDetails);
    }

    [HttpPost]
    public async Task<ActionResult<List<EventDetails>>> Post(EventDetails newEventDetail)
    {
        _context.EventDetails.Add(newEventDetail);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newEventDetail.Id }, newEventDetail);
        //return Ok(await _context.EventDetails.ToListAsync());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEventDetail(long id, EventDetails eventDetail)
    {
        if (id != eventDetail.Id)
        {
            return BadRequest();
        }

        _context.Entry(eventDetail).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventDetailExists(id))
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
    public async Task<IActionResult> DeleteEventDetail(long id)
    {
        var eventDetail = await _context.EventDetails.FindAsync(id);
        if (eventDetail == null)
        {
            return NotFound();
        }

        _context.EventDetails.Remove(eventDetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventDetailExists(long id)
    {
        return _context.EventDetails.Any(e => e.Id == id);
    }
}

