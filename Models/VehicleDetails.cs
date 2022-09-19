namespace BFS_backend.Models;

public class VehicleDetails
{
    public long? Id { get; set; }
    public string? vehRegNumber { get; set; }
    public string? vehMake { get; set; }
    public string? vehModel { get; set; }
    public string? vehCategory { get; set; }
    public string? vehBody { get; set; }
    public string? vehColour { get; set; }
    public string? vehFuelType { get; set; }
    public string? vehEngineCapacity { get; set; }
    public DateTime? vehRegDate { get; set; }
    public DateTime? vehV5CIssueDate { get; set; }
    public DateTime? vehTaxExpiryDate { get; set; }
    public DateTime? vehMotExpiryDate { get; set; }
}


