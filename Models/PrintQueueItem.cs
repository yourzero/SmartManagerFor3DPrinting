using SQLite;
using System;

namespace Manager_for_3_D_Printing.Models;

    public class PrintQueueItem
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ModelId { get; set; } = string.Empty;
        public string FilamentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

