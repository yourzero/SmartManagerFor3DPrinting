using System.Collections.ObjectModel;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.ViewModels;

public class PrintQueueViewModel
{
    private readonly DatabaseContext db;

    public PrintQueueViewModel(DatabaseContext database)
    {
        db = database;
    }

    public ObservableCollection<PrintQueueItem> QueueItems { get; } = new();

    public async Task LoadQueueAsync()
    {
        var list = await db.Connection.Table<PrintQueueItem>().ToListAsync();
        QueueItems.Clear();
        foreach (var item in list)
            QueueItems.Add(item);
    }
}