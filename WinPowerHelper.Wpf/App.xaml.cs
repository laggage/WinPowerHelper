namespace WinPowerHelper
{
    using System;
    using System.Threading;
    using System.Windows;
    using WinPowerHelper.Wpf.Interaction;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (e, args) =>
            {
                _mutex = new Mutex(true, "WinPowerHelper", out bool isNew);
                if (!isNew)
                {
                    NativeMethods.PostMessage(
                        (IntPtr)NativeMethods.HWND_BROADCAST,
                        NativeMethods.WM_SHOWME,
                        IntPtr.Zero,
                        IntPtr.Zero);
                    Environment.Exit(0);
                }
            };
        }

        private Mutex _mutex;
    }
}
