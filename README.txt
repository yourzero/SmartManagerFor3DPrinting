Import-via-URL Integration - README

1. **Register services** in MauiProgram.cs:
   builder.Services.AddHttpClient<IModelImporter, UrlModelImporter>();

2. **Ensure DatabaseContext** is registered:
   builder.Services.AddSingleton<DatabaseContext>();

3. **ModelBrowserPage**:
   - XAML now has "Import from URL" button.
   - Code-behind handles URL prompt and import logic.
