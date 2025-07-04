using System.Collections.ObjectModel;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.ViewModels;

public class ModelBrowserViewModel(DatabaseContext database)
{
    public ObservableCollection<Model> Models { get; } = new();

    public async Task LoadModelsAsync()
    {
        var list = await database.Connection.Table<Model>().ToListAsync();
        Models.Clear();
        foreach (var m in list)
            Models.Add(m);
    }
}