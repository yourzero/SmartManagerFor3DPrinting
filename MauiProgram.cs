using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Services;
using Manager_for_3_D_Printing.ViewModels;
using Manager_for_3_D_Printing.Views;

namespace Manager_for_3_D_Printing
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { /* fonts config */ });

            // DB and import services
            //builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<ModelImportService>();
            builder.Services.AddSingleton<IFileModelImporter, FileModelImporter>();
            //builder.Services.AddSingleton<IUrlModelImporter, UrlModelImporter>();
            builder.Services.AddHttpClient<IUrlModelImporter, UrlModelImporter>();
            
            builder.Services.AddSingleton<DatabaseContext>(sp => new DatabaseContext(Path.Combine(FileSystem.AppDataDirectory, "app.db")));

            
            // ViewModels and Views
            builder.Services.AddTransient<ModelBrowserViewModel>();
            builder.Services.AddTransient<ModelBrowserPage>();
            

            var x = builder.Build();

            App.ServiceProvider = x.Services;
            
            return x;
        }
    }
}
