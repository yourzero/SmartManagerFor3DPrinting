using SQLite;
using System;

namespace Manager_for_3_D_Printing.Models
{
    public class ModelFile
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ModelId { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
