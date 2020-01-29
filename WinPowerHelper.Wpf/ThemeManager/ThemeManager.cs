namespace WinPowerHelper.Wpf.ThemeManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Resources;
    using System.Text;
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

        private static void EnsureThemes()
        {
            if (Themes.Count > 0) return;
            var assembly = typeof(ThemeManager).Assembly;
            var assemblyName = assembly.GetName();
            var resourceNames = assembly.GetManifestResourceNames();

            foreach (var resourceName in resourceNames)
            {
                if (!resourceName.EndsWith(".g.resources", StringComparison.OrdinalIgnoreCase))
                    continue;

                using (var reader = new ResourceReader(assembly.GetManifestResourceStream(resourceName)))
                {
                    foreach (DictionaryEntry entry in reader)
                    {
                        throw new NotImplementedException();
                    }
                }
                
                
            }
        }

    }
}
