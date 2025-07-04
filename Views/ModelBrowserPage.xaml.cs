using Manager_for_3_D_Printing.ViewModels;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Task.Run(async () => await vm.LoadModelsAsync());
        if (BindingContext is ModelBrowserViewModel vm)
            await vm.LoadModelsAsync();
    }
}