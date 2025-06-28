using Data;
using Microsoft.Extensions.Logging;

namespace Manager_for_3_D_Printing;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "model_library.db");
        builder.Services.AddSingleton(new DatabaseContext(dbPath));


        return builder.Build();
    }
}