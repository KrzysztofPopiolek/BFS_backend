namespace BFS_backend.Models;

public class TaxRate
{
    public long? Id { get; set; }
    public string? bandName { get; set; }
    public double taxableValue { get; set; }
    public float taxRate { get; set; }
}


