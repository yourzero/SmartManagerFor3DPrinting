namespace Manager_for_3_D_Printing.Models;

public class ModelVersion
{
    public int Id { get; set; }
    public string ModelId { get; set; }
    public string Notes { get; set; }
    public string VersionLabel { get; set; }
    public DateTime CreatedAt { get; set; }
}