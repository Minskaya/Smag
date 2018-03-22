using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace DecimalMarkupExtension
{
    public class DecimalAttachedBehavior : Behavior<TextBox>
    {
        // Using a DependencyProperty as the backing store for Precision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(int), typeof(DecimalAttachedBehavior), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for ScalingFactor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScalingFactorProperty =
            DependencyProperty.Register("ScalingFactor", typeof(ScalingFactor), typeof(DecimalAttachedBehavior), new PropertyMetadata(ScalingFactor.None));

        public int Precision
        {
            get { return (int)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        public ScalingFactor ScalingFactor
        {
            get { return (ScalingFactor)GetValue(ScalingFactorProperty); }
            set { SetValue(ScalingFactorProperty, value); }
        }

        public static T GetValue<T>(BindingExpression expression, object dataItem)
        {
            if (expression == null || dataItem == null)
            {
                return default(T);
            }

            string bindingPath = expression.ParentBinding.Path.Path;
            string[] properties = bindingPath.Split('.');

            object currentObject = dataItem;
            Type currentType = null;

            for (int i = 0; i < properties.Length; i++)
            {
                currentType = currentObject.GetType();
                PropertyInfo property = currentType.GetProperty(properties[i]);
                if (property == null)
                {
                    currentObject = null;
                    break;
                }
                currentObject = property.GetValue(currentObject, null);
                if (currentObject == null)
                {
                    break;
                }
            }

            return (T)currentObject;
        }

        protected override void OnAttached()
        {
            var be = GetTextBinding();
            be.ParentBinding.StringFormat = "{0:N1}";
        }

        private BindingExpression GetTextBinding()
        {
            return this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
        }
    }
}