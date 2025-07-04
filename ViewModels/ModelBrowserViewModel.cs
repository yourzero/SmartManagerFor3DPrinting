using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Manager_for_3_D_Printing.Data;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Manager_for_3_D_Printing.Models;
using Manager_for_3_D_Printing.Services;

namespace Manager_for_3_D_Printing.ViewModels
{
    public class ModelBrowserViewModel : BaseViewModel
    {
        private readonly DatabaseContext _database;
        private readonly IFileModelImporter fileImporter;
        public ObservableCollection<Model> Models { get; } = new();

        public ICommand ImportModelCommand { get; }
        //public string FilterText { get; set; }

        public ModelBrowserViewModel(DatabaseContext database, IFileModelImporter fileImporter)
        {
            _database = database;
            this.fileImporter = fileImporter;
            ImportModelCommand = new Command(async () => await ImportFromFileAsync());
        }

        private async Task ImportFromFileAsync()
        {
            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a 3D model",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    // include all the platforms you target
                    { DevicePlatform.WinUI, new[] { ".stl", ".3mf", ".gcode" } },
                    { DevicePlatform.Android, new[] { ".stl", ".3mf", ".gcode" } },
                    { DevicePlatform.iOS, new[] { ".stl", ".3mf", ".gcode" } }
                })
            });
            if (file == null)
                return;

            await fileImporter.ImportFromFileAsync(file);
            await LoadModelsAsync();
        }

        public async Task LoadModelsAsync()
        {
            var list = await _database.Connection.Table<Model>().ToListAsync();
            Models.Clear();
            foreach (var m in list)
                Models.Add(m);
        }
    }
}
