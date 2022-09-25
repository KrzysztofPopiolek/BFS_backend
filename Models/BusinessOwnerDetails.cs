namespace BFS_backend.Models;

public class BusinessOwnerDetails
{
    public long? Id { get; set; }
    public string? businessName { get; set; }
    public string? businessAddress { get; set; }
    public string? businessUTR { get; set; }
    public string? businessEstablishmentDate { get; set; }
    public string? ownerName { get; set; }
    public string? ownerNin { get; set; }
    public string? ownerHmrcId { get; set; }
    public string? ownerContact { get; set; }
    public DateTime? creationDate { get; set; }
}