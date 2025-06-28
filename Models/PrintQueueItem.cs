namespace Models;

public class PrintQueueItem
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public string FilamentType { get; set; }
    public string FilamentColor { get; set; }
    public string Status { get; set; } // Queued, Printed, Failed, Deferred
    public DateTime? DueDate { get; set; }
    public string Notes { get; set; }
    public int? EstimatedPrintTimeMinutes { get; set; }
    public int Priority { get; set; }
}
