namespace WinPowerHelper.Wpf.Views
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Time.xaml
    /// </summary>
    public partial class TimerControl : UserControl, INotifyPropertyChanged
    {
        public TimerControl()
        {
            InitializeComponent();
        }

        private Timer _timer = null;
        public event EventHandler TimingOver;

        #region DependencyProperty

        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register(
                "Interval", typeof(TimeSpan),
                typeof(TimerControl),
                new PropertyMetadata(
                    default(TimeSpan), OnIntervalChanged));
        private static void OnIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TimerControl t)) return;
            TimeSpan newValue = default;
            try
            {
                newValue = (TimeSpan)e.NewValue;
            }
            catch { }

            t.Hour1 = (byte)(newValue.Hours / 10);
            t.Hour0 = (byte)(newValue.Hours % 10);

            t.Minute1 = (byte)(newValue.Minutes / 10);
            t.Minute0 = (byte)(newValue.Minutes % 10);

            t.Second1 = (byte)(newValue.Seconds / 10);
            t.Second0 = (byte)(newValue.Seconds % 10);
        }
        #endregion

        #region NotifyProperty

        private byte _hour1;
        public byte Hour1
        {
            get => _hour1;
            set => Set(ref _hour1, value);
        }

        private byte _hour0;
        public byte Hour0
        {
            get => _hour0;
            set => Set(ref _hour0, value);
        }

        private byte _minute0;
        public byte Minute0
        {
            get => _minute0;
            set => Set(ref _minute0, value);
        }

        private byte _minute1;
        public byte Minute1
        {
            get => _minute1;
            set => Set(ref _minute1, value);
        }

        private byte _second0;
        public byte Second0
        {
            get => _second0;
            set => Set(ref _second0, value);
        }

        private byte _second1;
        public byte Second1
        {
            get => _second1;
            set => Set(ref _second1, value);
        }

        private bool _isTiming;
        /// <summary>
        /// 指示当前控件是否正处于倒计时状态
        /// </summary>
        public bool IsTiming
        {
            get => _isTiming;
            set => Set(ref _isTiming, value);
        }

        #endregion

        /// <summary>
        /// 开始计时
        /// </summary>
        public void BeginTiming()
        {
            if (_timer == null)
            {
                _timer = new Timer(1000);
                _timer.Elapsed += async (s, e) =>
                {
                    await Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Interval = Interval.Subtract(TimeSpan.FromSeconds(1));
                        if (Interval.TotalSeconds <= 0)
                        {
                            _timer?.Stop();
                            _timer?.Dispose();
                            _timer = null;
                            Interval = default;
                            IsTiming = false;
                            TimingOver?.Invoke(this, null);
                        }
                    }), null);
                };
                IsTiming = true;
                _timer.Start();
            }
        }

        /// <summary>
        /// 终止计时
        /// </summary>
        public void StopTiming()
        {
            if (IsTiming == false) return;
            IsTiming = false;
            _timer?.Stop();
            _timer?.Dispose();
            Dispatcher.Invoke(
                new Action(() =>
                {
                    Interval = default;
                }));
        }

        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        internal void Set<T>(ref T value, T newValue, [CallerMemberName]string propertyName = "")
        {
            value = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
