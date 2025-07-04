using System;
using Microsoft.Maui.Controls;
using Manager_for_3_D_Printing.Services;
using Manager_for_3_D_Printing.ViewModels;

namespace Manager_for_3_D_Printing.Views
{
    public partial class ModelBrowserPage : ContentPage
    {
        private readonly IModelImporter modelImporter;

        public ModelBrowserPage(IModelImporter modelImporter)
        {
            InitializeComponent();
            this.modelImporter = modelImporter;
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
                await modelImporter.ImportFromUrlAsync(url);
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
