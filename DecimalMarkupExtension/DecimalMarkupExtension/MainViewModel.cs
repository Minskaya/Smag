using System;
using System.Globalization;
using System.Windows.Controls;
using Prism.Mvvm;

namespace DecimalMarkupExtension
{
    public class MainViewModel : BindableBase
    {
        /// <summary>
        /// Culture />
        /// </summary>
        private ComboBoxItem _culture;

        /// <summary>
        /// value />
        /// </summary>
        private double _pi;

        /// <summary>
        /// precision />
        /// </summary>
        private int _precision;

        public MainViewModel()
        {
            this._pi = Math.PI;
            this.Area = 912345; //91 Ha
            this.Culture = new ComboBoxItem() { Content = "fr-FR" };
        }

        public int Area { get; private set; }

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public ComboBoxItem Culture
        {
            get
            {
                return _culture;
            }

            set
            {
                this.SetProperty(ref _culture, value);
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag((string)_culture.Content);
                this.RaisePropertyChanged("");
            }
        }

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public double Pi
        {
            get
            {
                return _pi;
            }

            set
            {
                this.SetProperty(ref _pi, value);
            }
        }

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public int Precision
        {
            get
            {
                return _precision;
            }

            set
            {
                this.SetProperty(ref _precision, value);
            }
        }
    }
}