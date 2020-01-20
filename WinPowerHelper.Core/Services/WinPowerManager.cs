namespace WinPowerHelper.Core.Services
{
    using System;
    using System.Diagnostics;
    using WinPowerHelper.Core.Interfaces;

    public class WinPowerManager: IPowerManager
    {
        private const string ShutdownCmd = "shutdown.exe";

        private WinPowerManager() { }

        private static readonly object _lock = new object();
        private static WinPowerManager _instance;
        public static WinPowerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null) 
                            _instance = new WinPowerManager();
                    }
                }

                return _instance;
            }
        }

        public void Shutdown()
        {
            Process.Start(ShutdownCmd, "-s");
        }

        public void Shutdown(TimeSpan interval)
        {
            Process.Start(ShutdownCmd, $"-s -t {interval.TotalSeconds}");
        }

        public void Sleep()
        {
            Process.Start(ShutdownCmd, "-h");
        }

        public void Sleep(TimeSpan interval)
        {
            Process.Start(ShutdownCmd, $"-h -t {interval.TotalSeconds}");
        }

        public void Restart()
        {
            Process.Start(ShutdownCmd, "-r");
        }

        public void Restart(TimeSpan interval)
        {
            Process.Start(ShutdownCmd, $"-r -t {interval.TotalSeconds}");
        }
    }
}
