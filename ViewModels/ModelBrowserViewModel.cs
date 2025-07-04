using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.ViewModels
{
    public class ModelBrowserViewModel
    {
        private readonly DatabaseContext db;
        public ObservableCollection<Model> Models { get; } = new ObservableCollection<Model>();

        public ModelBrowserViewModel(DatabaseContext database)
        {
            db = database;
        }

        public async Task LoadModelsAsync()
        {
            var list = await db.Connection.Table<Model>().ToListAsync();
            Models.Clear();
            foreach (var m in list)
                Models.Add(m);
        }
    }
}
