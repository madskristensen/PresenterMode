using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Community.VisualStudio.Toolkit;

namespace PresenterMode
{
    public class WindowResizer
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        public static async Task ApplyAsync()
        {
            General settings = await General.GetLiveInstanceAsync();

            if (!settings.ResizeVisualStudio)
            {
                return;
            }

            await VS.StatusBar.ShowMessageAsync("Resizing window...");
            
            Application.Current.MainWindow.WindowState = WindowState.Normal;

            if (PresentationSource.FromVisual(Application.Current.MainWindow) is HwndSource source)
            {
                IntPtr handle = source.Handle;
                MoveWindow(handle, 0, 0, settings.Width, settings.Height, false);
            }
        }
    }
}
