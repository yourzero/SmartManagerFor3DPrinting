using System.Collections.ObjectModel;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;
using System.Windows.Input;
using Manager_for_3_D_Printing.Encryption;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

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
    
    public ICommand ImportModelCommand => new Command(async () =>
    {
        var pick = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select 3D Model",
            //FileTypes = FilePickerFileType.Custom,
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                // include all the platforms you target
                { DevicePlatform.WinUI, new[] { ".stl", ".3mf", ".gcode" } },
                { DevicePlatform.Android, new[] { ".stl", ".3mf", ".gcode" } },
                { DevicePlatform.iOS, new[] { ".stl", ".3mf", ".gcode" } }
            })
        });
        if (pick == null) 
            return;
        
        // insert into DB (note: “database” is your primary-ctor field)
        var model = new Model
        {
            Name          = Path.GetFileNameWithoutExtension(pick.FileName),
            SourceUrl = null, // no website to come from
            
            //SourceUrl     = dest,
            DateAdded     = DateTime.UtcNow,
            //PreviewImagePath = "",    // fill later
            // ...other required props
        };

        // copy into your library folder
        var libDir = Path.Combine(FileSystem.AppDataDirectory, "Models");
        var modelDir = Path.Combine(libDir, model.Id);
        Directory.CreateDirectory(modelDir);
        
        var dest = Path.Combine(modelDir, pick.FileName);
        await using var src = await pick.OpenReadAsync();
        await using var dst = File.OpenWrite(dest);
        await src.CopyToAsync(dst);

        model.FileHash = Hashing.ComputeSha256Hash(src);
        model.FullRootFolderPath = modelDir;
        model.FolderName = model.Id;
        model.FileName = dest;
       
        await database.InsertModelAsync(model);

        // refresh UI
        await LoadModelsAsync();
    });

}