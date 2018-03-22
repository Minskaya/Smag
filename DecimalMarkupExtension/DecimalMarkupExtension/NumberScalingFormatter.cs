using System;
using System.Text;

namespace DecimalMarkupExtension
{
    public class NumberScalingFormatter : IFormatProvider, ICustomFormatter
    {
        private readonly ScalingFactor _scalingFactor;
        private readonly System.Globalization.CultureInfo _underlyingCulture;

        public NumberScalingFormatter(ScalingFactor scalingFactor, System.Globalization.CultureInfo underlyingCulture)
        {
            if (underlyingCulture == null)
            {
                throw new ArgumentNullException();
            }
            _scalingFactor = scalingFactor;
            _underlyingCulture = underlyingCulture;
        }

        public System.Globalization.CultureInfo Culture
        {
            get
            {
                return _underlyingCulture;
            }
        }

        public ScalingFactor Factor
        {
            get
            {
                return _scalingFactor;
            }
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
                formatter = _underlyingCulture.GetFormat(formatType);
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
                formattableString.Append(format);
            }
            formattableString.Append("}");

            return string.Format(_underlyingCulture, formattableString.ToString(), Scale(arg));
        }

        private int GetUnderlyingThousandScalingFactor()
        {
            int underlyingThousandScalingFactor;

            switch (_scalingFactor)
            {
                case ScalingFactor.None:
                    underlyingThousandScalingFactor = 0;
                    break;

                case ScalingFactor.Billion:
                    underlyingThousandScalingFactor = 3;
                    break;

                default:
                case ScalingFactor.Million:
                    underlyingThousandScalingFactor = 2;
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
                int underlyingThousandScalingFactor = GetUnderlyingThousandScalingFactor();
                try
                {
                    double convertedValue = Convert.ToDouble(arg);
                    scaledValue = Math.Pow(10, underlyingThousandScalingFactor * -3) * convertedValue;
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