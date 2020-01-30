namespace WinPowerHelper.Wpf.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using WinPowerHelper.Core.Interfaces;
    using WinPowerHelper.Core.Models;
    using WinPowerHelper.Core.Services;
    using WinPowerHelper.Wpf.Views;
    using ThemeManager;
    using System.Windows;
    using System.Linq;

    internal class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            SetIntervalCmd = new DelegateCommand<string>(SetInterval);
            SetIntervalToZeroCmd = new DelegateCommand(SetIntervalToZero);
            BeginOrStopTimingCmd = new DelegateCommand<TimerControl>(BeginOrStopTiming);

            PowerOptions = new List<PowerOptions>
            {
                Core.Models.PowerOptions.Sleep,
                Core.Models.PowerOptions.Shutdown,
                Core.Models.PowerOptions.Restart,
            };
            Themes = ThemeManager.Themes;
            SelectedTheme =
                Themes.FirstOrDefault(
                    x => x.ColorScheme.Contains(
                        "blue", StringComparison.OrdinalIgnoreCase));
        }

        public static string AppTitle => "Windows定时关机助手";

        private TimeSpan _interval;
        public TimeSpan Interval
        {
            get => _interval;
            set => SetProperty(ref _interval, value);
        }

        public IList<Theme> Themes { get; set; }
        public List<PowerOptions> PowerOptions { get; set; }
        public PowerOptions SelectedPowerOption { get; set; }

        private Theme _selectedTheme;
        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                SetProperty(ref _selectedTheme, value);
                ThemeManager.ChangeThemeColorScheme(Application.Current.Resources, value.ColorScheme);
            }
        }

        public ICommand SetIntervalCmd { get; private set; }
        public ICommand SetIntervalToZeroCmd { get; private set; }
        public ICommand BeginOrStopTimingCmd { get; private set; }

        private void SetInterval(string interval)
        {
            string[] splits = interval.Split(':');
            double seconds = 0;
            for (int i = 0; i < splits.Length; i++)
            {
                seconds += int.Parse(splits[i]) * Math.Pow(60, splits.Length - 1 - i);
            }

            Interval = Interval.Add(TimeSpan.FromSeconds(seconds));
        }

        private void SetIntervalToZero()
        {
            Interval = new TimeSpan(0);
        }

        private void BeginOrStopTiming(TimerControl timerControl)
        {
            if (timerControl.IsTiming) StopTiming(timerControl);
            else BeginTiming(timerControl);

        }

        private void StopTiming(TimerControl timerControl)
        {
            timerControl.StopTiming();
        }

        private void BeginTiming(TimerControl timerControl)
        {
            timerControl.BeginTiming();
            timerControl.TimingOver += (s, e) =>
            {
                IPowerManager powerManager = WinPowerManager.Instance;
                switch (SelectedPowerOption)
                {
                    case Core.Models.PowerOptions.Restart:
                        powerManager.Restart();
                        break;
                    case Core.Models.PowerOptions.Sleep:
                        powerManager.Sleep();
                        break;
                    case Core.Models.PowerOptions.Shutdown:
                        powerManager.Shutdown();
                        break;
                    default: break;
                }
            };
        }
    }
}
