using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using Manager_for_3_D_Printing.ViewModels;
using System.Threading.Tasks;

namespace Manager_for_3_D_Printing.Views;

    public partial class ModelBrowserPage : ContentPage
    {
        private readonly ModelBrowserViewModel vm;

        public ModelBrowserPage()
        {
            InitializeComponent();
            vm = App.ServiceProvider.GetService<ModelBrowserViewModel>()!;
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () => await vm.LoadModelsAsync());
        }
    }

