namespace BFS_backend;

public class EventDetails
{
    public int Id  { get; set; }
    public DateTime? EventDate { get; set; }
    public string? EvidenceNumberName { get; set; }
    public string? ContractorName { get; set; }
    public string? ContractorAddress { get; set; }
    public string? Account { get; set; }
    public string? TransactionType { get; set; }
    public double EventValue { get; set; }
}
