using Data;
using Models;

namespace Manager_for_3_D_Printing;

public partial class MainPage : ContentPage
{

    private readonly DatabaseContext _db;
    
    int count = 0;

    public MainPage(DatabaseContext db)
    {
        InitializeComponent();
        _db = db;
    }
    
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private async void OnAddModelClicked(object? sender, EventArgs e)
    {
        var newModel = new Model
        {
            Name = "Test Model",
            DateAdded = DateTime.UtcNow
        };
        await _db.InsertModelAsync(newModel); // Add InsertModelAsync method to your DB context
    }
}