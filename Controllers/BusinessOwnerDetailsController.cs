using BFS_backend.Data;
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
    public async Task<ActionResult<List<ContractorDetailsConst>>> Get()
    {
        return Ok(await _context.BusinessOwnerDetails.FirstAsync());
    }
}