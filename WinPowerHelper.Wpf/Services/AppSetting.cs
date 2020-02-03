namespace WinPowerHelper.Wpf.Services
{
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using WinPowerHelper.Core.Models;

    internal class AppSetting
    {
        private AppSetting()
        {
        }

        public const string SettingFileName = "appSettings.json";

        public string ThemeName { get; set; }
        public PowerOptions PowerOption { get; set; }

        private static object _syncRoot = new object();
        private static AppSetting _instance;
        public static AppSetting Instance
        {
            get
            {
                if(_instance == null)
                    lock(_syncRoot)
                        if (_instance == null)
                        {
                            if (!File.Exists(SettingFileName))
                            {
                                using var fs = File.Create(SettingFileName);
                                fs.Close();
                            }
                            string json = File.ReadAllText(SettingFileName);
                            if (string.IsNullOrEmpty(json)) json = "{}";
                            _instance = JsonSerializer.Deserialize<AppSetting>(json);
                        }
                return _instance;
            }
            set => _instance = value;
        }

        public static void SaveSettings()
        {
            lock(_syncRoot)
            {
                File.WriteAllText(
                    SettingFileName,
                    JsonSerializer.Serialize(_instance));
            }
        }

        public static Task SaveSettingsAsync()
        {
            return Task.Run(() => SaveSettings());
        }
    }
}
