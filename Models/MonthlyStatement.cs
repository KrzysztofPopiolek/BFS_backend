namespace BFS_backend.Models;

public class MonthlyStatement
{
    public MonthlyStatement(DateTime monthYear, double income, double expenses, double balance, double currentBalance, string comment)
	{
        this.monthYear = monthYear;
        this.income = income;
        this.expenses = expenses;
        this.balance = balance;
        this.currentBalance = currentBalance;
        this.comment = comment;
	}
    public long? Id { get; set; }
    public DateTime monthYear { get; set; }
    public double income { get; set; }
    public double expenses { get; set; }
    public double balance { get; set; }
    public double currentBalance { get; set; }
    public string? comment { get; set; }
}

