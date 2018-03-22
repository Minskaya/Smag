using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DecimalMarkupExtension
{
    public enum ScalingFactor
    {
        None,
        Thousand,
        TenThousand,
        HundredThousand,
        Million,
        Billion
    }

    [MarkupExtensionReturnType(typeof(string))]
    public class DecimalBindingExtension : BindingDecoratorBase
    {
        public DecimalBindingExtension()
        {
        }

        public DecimalBindingExtension(string path)
            : base(path)
        {
        }

        public int Precision { get; set; }

        public ScalingFactor ScalingFactor { get; set; }

        // <summary>
        /// This method is being invoked during initialization.
        /// </summary>
        /// <param name="provider">Provides access to the bound items.</param>
        /// <returns>
        /// The binding expression that is created by the base class.
        /// </returns>
        public override object ProvideValue(IServiceProvider provider)
        {
            //delegate binding creation etc. to the base class
            BindingExpression val = base.ProvideValue(provider) as BindingExpression;

            NumberScalingFormatter formatter = new NumberScalingFormatter(
                this.ScalingFactor,
                CultureInfo.CurrentCulture);

            //try to get bound items for our custom work
            DependencyObject targetObject;
            DependencyProperty targetProperty;
            bool status = TryGetTargetItems(provider, out targetObject, out targetProperty);

            if (targetObject == null)
            {
                // Cas d'un template
                return this;
            }

            if (status)
            {
                Binding b;
            }
            //val = "toto";
            return val;
        }
    }
}