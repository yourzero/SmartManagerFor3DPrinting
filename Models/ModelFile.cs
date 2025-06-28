namespace Models;

public class ModelFile
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; } // STL, 3MF, GCODE
    public string RelativePath { get; set; } // Relative to model folder
    public DateTime DateAdded { get; set; }
}
