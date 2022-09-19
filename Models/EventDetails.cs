namespace BFS_backend.Models;

public class EventDetails
{
    public long? Id { get; set; }
    public DateTime? EventDate { get; set; }
    public string? EvidenceNumberName { get; set; }
    public string? ContractorName { get; set; }
    public string? ContractorAddress { get; set; }
    public string? Description { get; set; }
    public string? TransferType { get; set; }
    public string? TransactionType { get; set; }
    public double EventValue { get; set; }
}
