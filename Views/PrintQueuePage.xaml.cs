using Microsoft.Maui.Controls;
using Manager_for_3_D_Printing.ViewModels;
using System.Threading.Tasks;

namespace Manager_for_3_D_Printing.Views
{
    public partial class PrintQueuePage : ContentPage
    {
        private readonly PrintQueueViewModel vm;
        public PrintQueuePage()
        {
            InitializeComponent();
            vm = App.ServiceProvider.GetService<PrintQueueViewModel>();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () => await vm.LoadQueueAsync());
        }
    }
}
