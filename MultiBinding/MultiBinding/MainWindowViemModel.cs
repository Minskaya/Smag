using Prism.Mvvm;

namespace MultiBinding
{
    public class MainWindowViemModel : BindableBase
    {
        private double _Double;

        private string _Text;

        public MainWindowViemModel()
        {
            this._Double = 0.12345;
            this._Text = "0.12345";
        }

        public double Double
        {
            get
            {
                return _Double;
            }

            set
            {
                this.SetProperty(ref _Double, value);
            }
        }

        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                this.SetProperty(ref _Text, value);
            }
        }

        public double toto => 0.1;
    }
}