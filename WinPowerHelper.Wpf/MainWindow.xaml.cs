namespace WinPowerHelper
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using WinPowerHelper.Wpf.Interaction;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == NativeMethods.WM_SHOWME)
                Activate();
            return IntPtr.Zero;
        }
    }
}
