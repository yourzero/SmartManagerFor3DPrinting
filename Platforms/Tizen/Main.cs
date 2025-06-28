using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Manager_for_3_D_Printing;

class Program : MauiApplication
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}