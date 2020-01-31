namespace WinPowerHelper.Wpf.ThemeManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Windows;

    public static class ThemeManager
    {
        static ThemeManager()
        {
            Themes = new List<Theme>();
        }

        private static List<Theme> _themes;
        public static List<Theme> Themes
        {
            get
            {
                EnsureThemes();
                return _themes;
            }
            private set
            {
                _themes = value;
            }
        }

        /// <summary>
        /// 确保 <see cref="Themes"/> 存在数据
        /// </summary>
        /// <note>
        /// 这个方法中不能调用属性 <see cref="Themes"/>, 否则会造成 "循环引用"
        /// 这个方法会被一直递归, 直到堆栈溢出
        /// </note>
        private static void EnsureThemes()
        {
            if (_themes.Count > 0) return;
            var assembly = typeof(ThemeManager).Assembly;
            var assemblyName = assembly.GetName().Name;
            var resourceNames = assembly.GetManifestResourceNames();

            foreach (var resourceName in resourceNames)
            {
                if (!resourceName.EndsWith(".g.resources", StringComparison.OrdinalIgnoreCase))
                    continue;

                using (var reader = new ResourceReader(assembly.GetManifestResourceStream(resourceName)))
                {
                    foreach (DictionaryEntry entry in reader)
                    {
                        string stringKey = entry.Key as string;
                        if (stringKey == null ||
                            !stringKey.Contains("/themes/", StringComparison.OrdinalIgnoreCase) ||
                            !stringKey.EndsWith(".baml", StringComparison.OrdinalIgnoreCase) ||
                            stringKey.EndsWith("generic.baml", StringComparison.OrdinalIgnoreCase)) continue;

                        var resourceDictionary = new ResourceDictionary()
                        {
                            Source = new Uri(
                                $"pack://application:,,,/{assemblyName};component/{stringKey.Replace("baml", "xaml")}")
                        };

                        if (resourceDictionary != null && 
                            resourceDictionary.MergedDictionaries.Count == 0)
                            _themes.Add(new Theme(resourceDictionary));
                    }
                }
            }
        }

        public static void ChangeThemeColorScheme(ResourceDictionary resources, string newColorScheme)
        {
            var oldThemeResource = resources.MergedDictionaries.FirstOrDefault(
               x => x.Source.LocalPath.Contains(
                   "light.", StringComparison.OrdinalIgnoreCase));

            if (oldThemeResource[Theme.ThemeColorSchemeKey] as string == newColorScheme) return;

            var newTheme = Themes.FirstOrDefault(x =>
                x.ColorScheme.Equals(newColorScheme, StringComparison.OrdinalIgnoreCase));

            string assemblyName = typeof(ThemeManager).Assembly.GetName().Name;
            var newThemeResource = new ResourceDictionary
            {
                Source = new Uri(
                    $"pack://application:,,,/{assemblyName};component/Styles/Themes/Light.{newColorScheme}.xaml")
            };
            resources.MergedDictionaries.Add(newThemeResource);
            if (oldThemeResource != null)
                resources.MergedDictionaries.Remove(oldThemeResource);
        }
    }
}
