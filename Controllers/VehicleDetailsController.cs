using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleDetailsController : Controller
{
    private readonly DataContext _context;

    public VehicleDetailsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleDetails>>> Get()
    {
        return Ok(await _context.VehicleDetails.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<VehicleDetails>>> Post(VehicleDetails newVehicleDetails)
    {
        _context.VehicleDetails.Add(newVehicleDetails);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newVehicleDetails.Id }, newVehicleDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, VehicleDetails vehicleDetails)
    {
        if (id != vehicleDetails.Id)
        {
            return BadRequest();
        }

        _context.Entry(vehicleDetails).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VehicleDetailExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool VehicleDetailExists(long id)
    {
        return _context.VehicleDetails.Any(e => e.Id == id);
    }
}