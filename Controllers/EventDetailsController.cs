using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return Ok(await _context.EventDetails.OrderBy(x => x.EventDate).ToListAsync());
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
        await UpdateMonthlyStatement();
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newEventDetail.Id }, newEventDetail);
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

        await UpdateMonthlyStatement();
        await _context.SaveChangesAsync();

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
        await UpdateMonthlyStatement();
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EventDetailExists(long id)
    {
        return _context.EventDetails.Any(e => e.Id == id);
    }

	private async Task<bool> UpdateMonthlyStatement()
	{
		var monthlyStatements = await _context.MonthlyStatements.OrderBy(x => x.monthYear).ToListAsync();
        var eventDetails = await _context.EventDetails.OrderBy(x => x.EventDate).ToListAsync();

        foreach(var statement in monthlyStatements)
		{
            statement.income = 0;
            statement.expenses = 0;
            statement.balance = 0;
            statement.currentBalance = 0;
		}

        foreach (var eventDetail in eventDetails)
		{
			foreach (var statement in monthlyStatements)
			{
                if (statement.monthYear.Month == eventDetail.EventDate?.Month && statement.monthYear.Year == eventDetail.EventDate?.Year)
                {
                    if (eventDetail.TransactionType == "income")
                    {
                        statement.income += eventDetail.EventValue;
                    }
                    else if (eventDetail.TransactionType == "expense")
                    {
                        statement.expenses += eventDetail.EventValue;
                    }
                    statement.balance = statement.income - statement.expenses;
                    if(statement.balance > 0)
					{
                        statement.comment = "PROFIT";
					}
                    else if(statement.balance < 0)
					{
                        statement.comment = "LOSS";
					}
                    else
					{
                        statement.comment = "BREAK-EVEN";
                    }
                    _context.Entry(statement).State = EntityState.Modified;
                    break;
                }
			}
		}
        var index = 0;
        foreach (var statement in monthlyStatements)
		{
            for (int i = 0; i <= index; i++)
            {
                statement.currentBalance += monthlyStatements[i].balance;
            }
            index++;
        }
        return true;
    }
}

