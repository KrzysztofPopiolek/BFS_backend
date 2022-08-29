using BFS_backend.Data;
using BFS_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFS_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeExpensesRecordController : ControllerBase
{
    private readonly DataContext _context;
    public IncomeExpensesRecordController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<IncomeExpensesRecord>>> Get()
    {
        IncomeExpensesRecord output = new IncomeExpensesRecord();

        var eventRecords = _context.EventDetails;
        double incomeSum = 0;
        double expenseSum = 0;
        foreach (var record in eventRecords)
        {
            if (record.TransactionType == "income")
            {
                incomeSum += record.EventValue;
            }
            else if (record.TransactionType == "expense")
            {
                expenseSum += record.EventValue;
            }
        }
        var currentBalance = incomeSum - expenseSum;
        var balanceForecast = Math.Round(await calculateForecastBalance(currentBalance));

        output.totalIncome = Math.Round(incomeSum, 2);
        output.totalExpense = Math.Round(expenseSum, 2);
        output.balance = Math.Round(currentBalance, 2);
        output.taxDue = Math.Round(await calculateTaxFromBalanceAsync(output.balance), 2);
        output.taxForecast = Math.Round(await calculateTaxFromBalanceAsync(balanceForecast), 2);
        return Ok(output);
    }

    private async Task<double> calculateTaxFromBalanceAsync(double balance)
    {
        var taxRateRecords = await _context.TaxRates.ToListAsync();
        var tradingAllowance = 1000; // TODO put this in consts table
        var personalAllowanceIncomeThreshold = 100000; // TODO put in const table
        if (balance < taxRateRecords[1].taxableValue)
        {
            return 0;
        }

        var personalAllowance = taxRateRecords[1].taxableValue;
        if (balance > personalAllowanceIncomeThreshold)
        {
            personalAllowance = Math.Max(taxRateRecords[1].taxableValue - ((balance - tradingAllowance) - personalAllowanceIncomeThreshold) / 2, 0);
        }

        else if (balance > taxRateRecords[1].taxableValue && balance < taxRateRecords[2].taxableValue)
        {
            var basicTaxRate = (balance - taxRateRecords[1].taxableValue) * taxRateRecords[1].taxRate;

            return basicTaxRate;
        }
        else if (balance > taxRateRecords[2].taxableValue && balance < taxRateRecords[3].taxableValue)
        {
            var higherTaxRate = ((balance - tradingAllowance) - taxRateRecords[2].taxableValue + taxRateRecords[1].taxableValue - personalAllowance) * taxRateRecords[2].taxRate;
            var basicTaxRate = (taxRateRecords[2].taxableValue - taxRateRecords[1].taxableValue) * taxRateRecords[1].taxRate;

            return basicTaxRate + higherTaxRate;
        }
        else if (balance > taxRateRecords[3].taxableValue)
        {
            var additionalTaxRate = ((balance - tradingAllowance) - taxRateRecords[3].taxableValue) * taxRateRecords[3].taxRate;
            var higherTaxRate = ((taxRateRecords[3].taxableValue - taxRateRecords[2].taxableValue + taxRateRecords[1].taxableValue)) * taxRateRecords[2].taxRate;
            var basicTaxRate = (taxRateRecords[2].taxableValue - taxRateRecords[1].taxableValue) * taxRateRecords[1].taxRate;

            return basicTaxRate + higherTaxRate + additionalTaxRate;
        }
        return 0;
    }

    private async Task<double> calculateForecastBalance(double currentBalance)
    {
        var taxStartDate = (await _context.TaxYearDatesDetails.FirstAsync()).taxYearStartDate;
        var taxEndDate = (await _context.TaxYearDatesDetails.FirstAsync()).taxYearEndDate;
        int taxYearDaysTotal = (int)(taxEndDate - taxStartDate).Days + 1;
        var currentDate = DateTime.Now;
        int daysSinceTaxStartDate = (int)(currentDate - taxStartDate).Days + 1;
        return (taxYearDaysTotal * currentBalance) / daysSinceTaxStartDate;
    }
}