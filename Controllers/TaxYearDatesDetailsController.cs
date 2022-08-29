using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxYearDatesDetailsController : ControllerBase
{
    private readonly DataContext _context;
    public TaxYearDatesDetailsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaxYearDatesDetails>>> Get()
    {
        try
        {
            var taxYearDatesDetails = await _context.TaxYearDatesDetails.FirstAsync();
            return Ok(taxYearDatesDetails);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<TaxYearDatesDetails>>> Post(TaxYearDatesDetails newTaxYearDatesDetails)
    {
        _context.TaxYearDatesDetails.Add(newTaxYearDatesDetails);
        addMonthsToMonthlyStatements(newTaxYearDatesDetails);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newTaxYearDatesDetails.Id }, newTaxYearDatesDetails);
    }

    private async void addMonthsToMonthlyStatements(TaxYearDatesDetails newTaxYearDatesDetails)
	{
        var monthlyStatements = await _context.MonthlyStatements.ToListAsync();

        if (monthlyStatements.Count <= 0)
        {
            var target = new DateTime(newTaxYearDatesDetails.taxYearStartDate.Year,
                newTaxYearDatesDetails.taxYearStartDate.Month + 1,
                newTaxYearDatesDetails.taxYearStartDate.Day);

            var newDates = new List<DateTime>();
            while (target <= newTaxYearDatesDetails.taxYearEndDate)
            {
                var newDate = new DateTime(target.Year, target.Month, 1);
                newDates.Add(newDate);
                target = target.AddMonths(1);
            }

            var firstDate = new DateTime(
                newTaxYearDatesDetails.taxYearStartDate.Year,
                newTaxYearDatesDetails.taxYearStartDate.Month,
                newTaxYearDatesDetails.taxYearStartDate.Day);

            var firstStatement = new MonthlyStatement(firstDate, 0, 0, 0, 0, "");
            _context.MonthlyStatements.Add(firstStatement);

            foreach (var date in newDates)
            {
                var statementWithDate = new MonthlyStatement(date, 0, 0, 0, 0, "");
                _context.MonthlyStatements.Add(statementWithDate);
            }

            var lastDate = new DateTime(
                newTaxYearDatesDetails.taxYearEndDate.Year,
                newTaxYearDatesDetails.taxYearEndDate.Month, 
                newTaxYearDatesDetails.taxYearEndDate.Day);

            var lastStatement = new MonthlyStatement(lastDate, 0, 0, 0, 0, "");
            _context.MonthlyStatements.Add(lastStatement);
        }
    }
}