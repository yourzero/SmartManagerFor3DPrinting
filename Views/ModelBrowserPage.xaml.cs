using System;
using Manager_for_3_D_Printing.Data;
using Microsoft.Maui.Controls;
using Manager_for_3_D_Printing.Services;
using Manager_for_3_D_Printing.ViewModels;

namespace Manager_for_3_D_Printing.Views
{
    public partial class ModelBrowserPage : ContentPage
    {
        private readonly IUrlModelImporter _urlModelImporter;
        private readonly IFileModelImporter _fileImporter;
        private DatabaseContext _database;

        // public ModelBrowserPage(IUrlModelImporter urlModelImporter)
        // {
        //     InitializeComponent();
        //     this._urlModelImporter = urlModelImporter;
        // }
        public ModelBrowserPage() 
            : this(
                App.ServiceProvider.GetRequiredService<IUrlModelImporter>(),
                App.ServiceProvider.GetRequiredService<IFileModelImporter>(),
                App.ServiceProvider.GetRequiredService<DatabaseContext>())
        { }
        
        public ModelBrowserPage(
            IUrlModelImporter urlImporter,
            IFileModelImporter fileImporter, DatabaseContext database)
        {
            InitializeComponent();
            _urlModelImporter = urlImporter;
            _fileImporter = fileImporter;
            _database = database;
            BindingContext = new ModelBrowserViewModel(_database,_fileImporter);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ModelBrowserViewModel vm)
                await vm.LoadModelsAsync();
        }

        private async void OnImportFromUrlClicked(object sender, EventArgs e)
        {
            var url = await DisplayPromptAsync("Import Model", "Enter model URL:");
            if (string.IsNullOrWhiteSpace(url))
                return;

            try
            {
                await _urlModelImporter.ImportFromUrlAsync(url);
                if (BindingContext is ModelBrowserViewModel vm)
                    await vm.LoadModelsAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
