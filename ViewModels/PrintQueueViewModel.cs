using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;
using Models;
using SQLite;

namespace Manager_for_3_D_Printing.ViewModels
{
    public class PrintQueueViewModel
    {
        private readonly DatabaseContext db;
        public ObservableCollection<PrintQueueItem> QueueItems { get; } = new ObservableCollection<PrintQueueItem>();

        public PrintQueueViewModel(DatabaseContext database)
        {
            db = database;
        }

        public async Task LoadQueueAsync()
        {
            var list = await db.Connection.Table<PrintQueueItem>().ToListAsync();
            QueueItems.Clear();
            foreach (var item in list)
                QueueItems.Add(item);
        }
    }
}
