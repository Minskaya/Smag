using System;
using System.Globalization;
using System.Text;

namespace DecimalMarkupExtension
{
    public class NumberScalingFormatter : IFormatProvider, ICustomFormatter
    {
        private readonly System.Globalization.CultureInfo _underlyingCulture;
        private ScalingFactor _scalingFactor;

        public NumberScalingFormatter(ScalingFactor scalingFactor, System.Globalization.CultureInfo underlyingCulture)
        {
            if (underlyingCulture == null)
            {
                throw new ArgumentNullException();
            }

            _scalingFactor = scalingFactor;
            _underlyingCulture = underlyingCulture;
        }

        public NumberScalingFormatter(System.Globalization.CultureInfo underlyingCulture)
        {
            if (underlyingCulture == null)
            {
                throw new ArgumentNullException();
            }

            _scalingFactor = ScalingFactor.None;
            _underlyingCulture = underlyingCulture;
        }

        public NumberScalingFormatter()
        {
            _scalingFactor = ScalingFactor.None;
        }

        #region IFormatProvider Members

        public object GetFormat(Type formatType)
        {
            object formatter = null;
            if (formatType == typeof(ICustomFormatter))
            {
                formatter = this;
            }
            else
            {
                formatter = (_underlyingCulture ?? CultureInfo.CurrentCulture)
                            .GetFormat(formatType);
            }
            return formatter;
        }

        #endregion

        #region ICustomFormatter Members

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            StringBuilder formattableString = new StringBuilder("{0");
            if (!string.IsNullOrEmpty(format))
            {
                formattableString.Append(":");
                //formattableString.Append(format);

                var toto = format.Split(':');

                formattableString.Append(toto[0]);
                ScalingFactor scale;
                if (Enum.TryParse<ScalingFactor>(toto[1], out scale))
                {
                    this._scalingFactor = scale;
                }
            }
            formattableString.Append("}");

            return string.Format(
                _underlyingCulture ?? CultureInfo.CurrentCulture,
                formattableString.ToString(),
                Scale(arg));
        }

        private double GetUnderlyingThousandScalingFactor()
        {
            double underlyingThousandScalingFactor;

            switch (_scalingFactor)
            {
                case ScalingFactor.None:
                    underlyingThousandScalingFactor = 0;
                    break;

                case ScalingFactor.Thousand:
                    underlyingThousandScalingFactor = 1E-3;
                    break;

                case ScalingFactor.TenThousand:
                    underlyingThousandScalingFactor = 1E-4;
                    break;

                case ScalingFactor.HundredThousand:
                    underlyingThousandScalingFactor = 1E-5;
                    break;

                case ScalingFactor.Billion:
                    underlyingThousandScalingFactor = 1E-6;
                    break;

                default:
                case ScalingFactor.Million:
                    underlyingThousandScalingFactor = 1E-9;
                    break;
            }

            return underlyingThousandScalingFactor;
        }

        private object Scale(object arg)
        {
            object scaledValue = null;

            if (arg == null)
            {
                scaledValue = null;
            }
            else if (_scalingFactor == ScalingFactor.None)
            {
                scaledValue = arg;
            }
            else
            {
                double underlyingThousandScalingFactor = GetUnderlyingThousandScalingFactor();
                try
                {
                    double convertedValue = Convert.ToDouble(arg);
                    scaledValue = underlyingThousandScalingFactor * convertedValue;
                }
                catch (InvalidCastException)
                {
                }
            }
            return scaledValue;
        }

        #endregion
    }
}