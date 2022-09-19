namespace BFS_backend.Models;

public class MileageRecord
{
    public long? Id { get; set; }
    public DateTime RecordDate { get; set; }
    public string? Destination { get; set; }
    public string? Reason { get; set; }
    public long LastOdometerReading { get; set; }
    public long FinishOdometerReading { get; set; }
    public long? MilesOutOfService { get; set; }
    public long? mileage { get; set; }
}
