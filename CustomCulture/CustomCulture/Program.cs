using System;
using System.Globalization;
using Microsoft.Win32;

namespace CustomCulture
{
    internal class Program
    {
        public static void Main()
        {
            CultureInfo ci = new CultureInfo("fr-FR");
            double toto = 1.123456789;

            var formatprovider = ci.NumberFormat;
            formatprovider.NumberDecimalDigits = 4;

            Console.Write($"normal {toto} custom {toto.ToString(formatprovider)}");
            Console.ReadLine();

            SystemEvents.UserPreferenceChanged += (sender, e) =>
            {
                // Regional settings have changed
                if (e.Category == UserPreferenceCategory.Locale)
                {
                    // .NET also caches culture settings, so clear them
                    CultureInfo.CurrentCulture.ClearCachedData();

                    // do some other stuff
                }
            };
        }
    }
}