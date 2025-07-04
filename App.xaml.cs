using Application = Microsoft.Maui.Controls.Application;
using Window = Microsoft.Maui.Controls.Window;

#if WINDOWS
using WinRT.Interop;
using System.Runtime.InteropServices;
#endif

namespace Manager_for_3_D_Printing;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override Window CreateWindow(IActivationState state)
    {
        var window = base.CreateWindow(state);

        window.Width = 1000;
        window.Height = 700;
        window.MinimumWidth = 800;
        window.MinimumHeight = 600;

#if WINDOWS
        window.HandlerChanged += (s, e) =>
        {
            if (window.Handler.PlatformView is Window nativeWindow)
            {
                const int SM_CXSCREEN = 0, SM_CYSCREEN = 1, SWP_NOSIZE = 0x0001;
                var screenW = GetSystemMetrics(SM_CXSCREEN);
                var screenH = GetSystemMetrics(SM_CYSCREEN);
                var x = (screenW - (int)window.Width) / 2;
                var y = (screenH - (int)window.Height) / 2;
                var hWnd = WindowNative.GetWindowHandle(nativeWindow);
                SetWindowPos(hWnd, HWND_TOP, x, y, 0, 0, SWP_NOSIZE);
            }
        };
#endif

        return window;
    }

    internal static void SetServiceProvider(IServiceProvider sp)
    {
        ServiceProvider = sp;
    }

#if WINDOWS
    private static readonly IntPtr HWND_TOP = IntPtr.Zero;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
#endif
}