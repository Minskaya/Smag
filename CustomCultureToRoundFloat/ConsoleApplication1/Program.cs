using System;
using System.Globalization;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var x = 1.0 / 3;
            var y = 2.0 / 3;

            Console.WriteLine($"culture {currentCulture.IetfLanguageTag} NumberDecimalDigits {currentCulture.NumberFormat.NumberDecimalDigits} 1/3={x.ToString("f")} 2/3={y.ToString("f")}");

            CultureAndRegionInfoBuilder builder = new CultureAndRegionInfoBuilder("fr-FR-Mush", CultureAndRegionModifiers.None);
            CultureInfo parent = new CultureInfo("fr-FR");
            builder.LoadDataFromCultureInfo(parent);
            builder.LoadDataFromRegionInfo(new RegionInfo("FR"));
            builder.Parent = parent;
            builder.NumberFormat.NumberDecimalDigits = 4;
            builder.RegionNativeName = "Francais bouillie";
            builder.RegionEnglishName = "French mush";
            builder.Register();

            /*
             * NumberFormatInfo nfi = (NumberFormatInfo)currentCulture.NumberFormat.Clone();
            nfi.NumberDecimalDigits = 4;
            var customCulture = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            customCulture.NumberFormat = nfi;
            CultureInfo.CurrentCulture = customCulture;
            *
            * */
            CultureInfo frMush = new CultureInfo("fr-FR-Mush");
            Console.WriteLine($"culture {currentCulture.IetfLanguageTag} NumberDecimalDigits {currentCulture.NumberFormat.NumberDecimalDigits} 1/3={x.ToString("f", frMush)} 2/3={y.ToString("f", frMush)}");

            Console.ReadKey();
        }
    }
}