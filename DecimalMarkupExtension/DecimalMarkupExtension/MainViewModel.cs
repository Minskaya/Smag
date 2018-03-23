using System;
using System.Collections.ObjectModel;
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

            this.Items = new ObservableCollection<Item>()
            {
                new Item() { Variety = "Blé", Name= "Parcelle 1", Area=11111, HasContract = true },
                new Item() { Variety = "Blé", Name= "Parcelle 2", Area=22222 },
                new Item() { Variety = "Orge", Name= "Parcelle 3", Area=33333, HasContract = true },
                new Item() { Variety = "Orge", Name= "Parcelle 4", Area=44444 },
            };
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

        public ObservableCollection<Item> Items { get; private set; }

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