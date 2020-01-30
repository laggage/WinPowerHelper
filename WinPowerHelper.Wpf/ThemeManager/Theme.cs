namespace WinPowerHelper.Wpf.ThemeManager
{
    using System.Windows;
    using System.Windows.Media;

    public class Theme
    {
        public Theme()
        {
        }

        public Theme(ResourceDictionary resources)
        {
            Name = resources[ThemeNameKey] as string;
            DisplayName = resources[ThemeDisplayNameKey] as string;
            BaseColorScheme = resources[ThemeBaseColorSchemeKey] as string;
            ColorScheme = resources[ThemeColorSchemeKey] as string;
            ShowcaseBrush = resources[ThemeShowcaseBrushKey] as Brush;
        }

        public const string ThemeNameKey = "Theme.Name";
        public const string ThemeDisplayNameKey = "Theme.DisplayName";
        public const string ThemeBaseColorSchemeKey = "Theme.BaseColorScheme";
        public const string ThemeColorSchemeKey = "Theme.ColorScheme";
        public const string ThemeShowcaseBrushKey = "Theme.ShowcaseBrush";

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string BaseColorScheme { get; set; }
        public string ColorScheme { get; set; }
        public Brush ShowcaseBrush { get; set; }
    }
}
