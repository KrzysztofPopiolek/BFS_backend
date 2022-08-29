using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MileageRecordController : ControllerBase
{
    private readonly DataContext _context;
    public MileageRecordController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<MileageRecord>>> Get()
    {
        return Ok(await _context.MileageRecords.OrderBy(x => x.RecordDate).ToListAsync());
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<MileageRecord>> GetById(long id)
    {
        var singleRecord = await _context.MileageRecords.FindAsync(id);
        if (singleRecord == null)
        {
            return BadRequest("Details not found");
        }
        return Ok(singleRecord);
    }

    [HttpGet("getNeighboursByDate/{date}")]
    public async Task<ActionResult<MileageRecord[]>> GetNeighboursByDate(DateTime date)
    {
        var mileageRecords = await _context.MileageRecords.AsNoTracking().ToListAsync();
        Boolean addRecord = true;

        MileageRecord mileageRecordDouble = new MileageRecord();
        mileageRecordDouble.RecordDate = date;

        foreach (var record in mileageRecords)
        {
            if (record.RecordDate == date)
            {
                addRecord = false;
                break;
            }
        }
        if (addRecord)
        {
            mileageRecords.Add(mileageRecordDouble);
        }
        var mileageRecordsSorted = mileageRecords.OrderBy(x => x.RecordDate).ToArray();


        MileageRecord prevRecord = new MileageRecord();
        MileageRecord nextRecord = new MileageRecord();
        var index = 0;
        foreach (var record in mileageRecordsSorted)
        {
            if (record.RecordDate == date)
            {
                if (index > 0)
                {
                    prevRecord = mileageRecordsSorted[index - 1];
                }
                if (index + 1 < mileageRecordsSorted.Count())
                {
                    nextRecord = mileageRecordsSorted[index + 1];
                }
            }
            index++;
        }

        List<MileageRecord> outputRecords = new List<MileageRecord>();
        outputRecords.Add(prevRecord);
        outputRecords.Add(nextRecord);

        return Ok(outputRecords);
    }

    [HttpGet("getRecordWithDateExists/{date}")]
    public async Task<ActionResult<Boolean>> GetRecordWithDateExists(DateTime date)
	{
        var mileageRecords = await _context.MileageRecords.AsNoTracking().ToListAsync();
        var mileageRecordsSorted = mileageRecords.OrderBy(x => x.RecordDate).ToArray();
        foreach (var record in mileageRecordsSorted)
        {
            if (record.RecordDate == date)
            {
                return Ok(true);
            }
            else if(record.RecordDate > date)
			{
                return Ok(false);
			}
        }
        return Ok(false);
    }

    [HttpGet("getRecordsInMonthYear/{date}")]
    public async Task<ActionResult<List<MileageRecord>>> getRecordsInMonthYear(DateTime date)
    {
        var mileageRecords = await _context.MileageRecords.AsNoTracking().ToListAsync();
        var mileageRecordsSorted = mileageRecords.OrderBy(x => x.RecordDate).ToArray();
        var output = new List<MileageRecord>();
        foreach (var record in mileageRecordsSorted)
        {
            if (record.RecordDate.Month == date.Month && record.RecordDate.Year == date.Year)
            {
                output.Add(record);
            }
   //         if(record.RecordDate?.Month > date.Month)
			//{
   //             break;
			//}
        }
        return Ok(output);
    }

    [HttpPost]
    public async Task<ActionResult<List<MileageRecord>>> Post(MileageRecord newMileageRecord)
    {
        var validationResult = await ValidateRecordAsync(newMileageRecord, false);

        if (validationResult != null)
        {
            return validationResult;
        }

        if (newMileageRecord.MilesOutOfService != null)
        {
            newMileageRecord.LastOdometerReading = newMileageRecord.LastOdometerReading + (long)newMileageRecord.MilesOutOfService;
        }
        newMileageRecord.mileage = newMileageRecord.FinishOdometerReading - newMileageRecord.LastOdometerReading;

        _context.MileageRecords.Add(newMileageRecord);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newMileageRecord.Id }, newMileageRecord);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, MileageRecord mileageRecord)
    {
        if (id != mileageRecord.Id)
        {
            return BadRequest();
        }

        var validationResult = await ValidateRecordAsync(mileageRecord, true);
        if (validationResult != null)
        {
            return validationResult;
        }

        _context.Entry(mileageRecord).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MileageRecordExists(id))
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
    public async Task<IActionResult> Delete(long id)
    {
        var mileageReccord = await _context.MileageRecords.FindAsync(id);
        if (mileageReccord == null)
        {
            return NotFound();
        }

        _context.MileageRecords.Remove(mileageReccord);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MileageRecordExists(long id)
    {
        return _context.MileageRecords.Any(e => e.Id == id);
    }

    private async Task<ActionResult> ValidateRecordAsync(MileageRecord newMileageRecord, Boolean editMode)
    {
        var mileageRecords = await _context.MileageRecords.AsNoTracking().ToListAsync();
        var taxYearDates = await _context.TaxYearDatesDetails.FirstAsync();

        var dateBeforeTaxYearStartDate = DateTime.Compare(newMileageRecord.RecordDate, taxYearDates.taxYearStartDate);
        var dateAfterTaxYearEndDate = DateTime.Compare(newMileageRecord.RecordDate, taxYearDates.taxYearEndDate);

        if (!editMode)
        {
            mileageRecords.Add(newMileageRecord);
        }
        var mileageRecordsSorted = mileageRecords.OrderBy(x => x.RecordDate).ToArray();
        MileageRecord prevRecord = new MileageRecord();
        MileageRecord nextRecord = new MileageRecord();
        var index = 0;
        foreach (var record in mileageRecordsSorted)
        {
            if (record.RecordDate == newMileageRecord.RecordDate)
            {
                if (index > 0)
                {
                    prevRecord = mileageRecordsSorted[index - 1];
                }
                if (index + 1 < mileageRecordsSorted.Count())
                {
                    nextRecord = mileageRecordsSorted[index + 1];
                }
            }
            index++;
        }

        if (dateBeforeTaxYearStartDate < 0 || dateAfterTaxYearEndDate > 0)
        {
            return BadRequest("Dates must be within tax year");
        }

        if (!editMode)
        {
            if (newMileageRecord.RecordDate == prevRecord.RecordDate || newMileageRecord.RecordDate == nextRecord.RecordDate)
            {
                return BadRequest("Record with this date already exists");
            }
        }

        if (newMileageRecord.LastOdometerReading + (newMileageRecord.MilesOutOfService ?? 0) >= newMileageRecord.FinishOdometerReading)
        {
            return BadRequest("New records Last Odometer Reading + Miles Out of Service must be less than Finish Odometer Reading");
        }

        else if (prevRecord.Id != null)
        {
            if (newMileageRecord.LastOdometerReading < prevRecord.FinishOdometerReading)
            {
                return BadRequest("New records Last Odometer Reading must be greater or equal to previous records Finish Odometer Reading");
            }
        }
        else if (nextRecord.Id != null)
        {
            if (newMileageRecord.FinishOdometerReading > nextRecord.LastOdometerReading)
            {
                return BadRequest("New records Finish Odometer Reading must be less or equal to next records Last Odometer Reading");
            }
        }
        return null;
    }
}

