Import-via-URL Integration (Updated)

1. Register services in MauiProgram.cs:
   builder.Services.AddHttpClient<IModelImporter, UrlModelImporter>();
   builder.Services.AddSingleton<DatabaseContext>(sp => new DatabaseContext(Path.Combine(FileSystem.AppDataDirectory, "app.db")));

2. Ensure existing registrations for ModelBrowserPage, ViewModels, etc.

3. The UrlModelImporter now inserts both Model and ModelFile records,
   matching your current ModelFile.cs and Model.cs definitions.
