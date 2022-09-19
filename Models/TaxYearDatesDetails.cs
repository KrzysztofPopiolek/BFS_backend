namespace BFS_backend.Models;

public class TaxYearDatesDetails
{
    public long? Id { get; set; }
    public DateTime taxYearStartDate { get; set; }
    public DateTime taxYearEndDate { get; set; }
    public DateTime selfAssessmentDeadline { get; set; }
}


