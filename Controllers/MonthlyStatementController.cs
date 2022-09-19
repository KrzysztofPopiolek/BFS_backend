using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonthlyStatementController : Controller
{
	private readonly DataContext _context;

    public MonthlyStatementController(DataContext context)
    {
        _context = context;
    }

	[HttpGet]
	public async Task<ActionResult<List<MonthlyStatement>>> Get()
	{
		return Ok(await _context.MonthlyStatements.ToListAsync());
	}
}