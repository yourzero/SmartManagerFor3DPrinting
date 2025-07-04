using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System;
using Application = Microsoft.Maui.Controls.Application;
using Window = Microsoft.Maui.Controls.Window;

#if WINDOWS
using WinRT.Interop;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices;
#endif

namespace Manager_for_3_D_Printing
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

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
                    int screenW = GetSystemMetrics(SM_CXSCREEN);
                    int screenH = GetSystemMetrics(SM_CYSCREEN);
                    int x = (screenW  - (int)window.Width) / 2;
                    int y = (screenH - (int)window.Height) / 2;
                    IntPtr hWnd = WindowNative.GetWindowHandle(nativeWindow);
                    SetWindowPos(hWnd, HWND_TOP, x, y, 0, 0, SWP_NOSIZE);
                }
            };
#endif

            return window;
        }

#if WINDOWS
        static readonly IntPtr HWND_TOP = IntPtr.Zero;
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);
#endif

        internal static void SetServiceProvider(IServiceProvider sp) => ServiceProvider = sp;
    }
}
