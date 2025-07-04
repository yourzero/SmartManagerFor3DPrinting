using System.Diagnostics;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.ViewModels;

namespace Manager_for_3_D_Printing
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "models.db");
            builder.Services.AddSingleton(new DatabaseContext(dbPath));
            
            Debug.WriteLine($"DB file is at {dbPath}");

            
            builder.Services.AddSingleton<ModelBrowserViewModel>();
            builder.Services.AddSingleton<PrintQueueViewModel>();

            var app = builder.Build();
            App.SetServiceProvider(app.Services);
            return app;
        }
    }
}
