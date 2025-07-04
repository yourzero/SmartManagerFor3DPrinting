using SQLite;
using System;

namespace Manager_for_3_D_Printing.Models
{
    public class ModelTag
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ModelId { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
    }
}
