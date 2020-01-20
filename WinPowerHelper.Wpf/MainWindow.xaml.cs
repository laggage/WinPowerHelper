namespace WinPowerHelper
{
    using System.Windows;
    using WinPowerHelper.Core.Interfaces;
    using WinPowerHelper.Core.Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.BeginTiming();
            timer.TimingOver += (s, args) =>
            {
                IPowerManager powerManager = WinPowerManager.Instance;
                powerManager.Shutdown();
            };
        }
    }
}
