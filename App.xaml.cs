using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
#if WINDOWS
using System;
using System.Runtime.InteropServices;
using WinRT.Interop;
#endif

namespace Manager_for_3_D_Printing
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        internal static void SetServiceProvider(IServiceProvider sp) => ServiceProvider = sp;
        
        protected override Window CreateWindow(IActivationState state)
        {
            var window = base.CreateWindow(state);

            // 1) Set a default size & minimums
            window.Width = 1000;
            window.Height = 700;
            window.MinimumWidth = 800;
            window.MinimumHeight = 600;

#if WINDOWS
            // 2) Once the WinUI window has been created, center it
            window.HandlerChanged += (s, e) =>
            {
                // Get the native WinUI window
                var nativeWindow = window.Handler.PlatformView;
                var hWnd = WindowNative.GetWindowHandle(nativeWindow);
                nativeWindow.Activate(); // Ensure it’s active

                // PInvoke to get screen size and move window
                const int SM_CXSCREEN = 0, SM_CYSCREEN = 1;
                int screenW = GetSystemMetrics(SM_CXSCREEN);
                int screenH = GetSystemMetrics(SM_CYSCREEN);
                int x = (screenW  - (int)window.Width) / 2;
                int y = (screenH - (int)window.Height) / 2;

                const uint SWP_NOSIZE = 0x0001;
                SetWindowPos(hWnd, HWND_TOP, x, y, 0, 0, SWP_NOSIZE);
            };
#endif

            return window;
        }

#if WINDOWS
        // PInvoke definitions
        static readonly IntPtr HWND_TOP = IntPtr.Zero;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);
#endif
    }
}
