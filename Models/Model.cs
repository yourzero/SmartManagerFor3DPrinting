using SQLite;

namespace Manager_for_3_D_Printing.Models;

public class Model
{
    [PrimaryKey]
    // public string Id { get; set; } = Guid.NewGuid().ToString();
    // public string Name { get; set; } = string.Empty;
    // public string PreviewImagePath { get; set; } = string.Empty;


    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string? PreviewImagePath { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public string? SourceUrl { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? LastPrinted { get; set; }
}