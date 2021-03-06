﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Threading;

namespace DecimalMarkupExtension
{
    /// <summary>
    /// A base class for custom markup extension which provides properties
    /// that can be found on regular <see cref="Binding"/> markup extension.<br/>
    /// See: http://www.hardcodet.net/2008/04/wpf-custom-binding-class
    /// </summary>
    [MarkupExtensionReturnType(typeof(object))]
    public abstract class BindingDecoratorBase : MarkupExtension
    {
        /// <summary>
        /// The decorated binding class.
        /// </summary>
        private Binding binding = new Binding();

        /// <summary>
        /// Default constructeur
        /// </summary>
        public BindingDecoratorBase()
        {
        }

        /// <summary>
        /// Construteurs acceptant le path
        /// </summary>
        /// <param name="path"></param>
        public BindingDecoratorBase(string path)
        {
            if (path != null)
            {
                if (Dispatcher.CurrentDispatcher == null)
                {
                    throw new InvalidOperationException();
                }

                this.Path = new PropertyPath(path, null);
            }
        }

        #region properties

        [DefaultValue(null)]
        public object AsyncState
        {
            get { return binding.AsyncState; }
            set { binding.AsyncState = value; }
        }

        /// <summary>
        /// The decorated binding class.
        /// </summary>
        [Browsable(false)]
        public Binding Binding
        {
            get { return binding; }
            set { binding = value; }
        }

        [DefaultValue("")]
        public string BindingGroupName
        {
            get { return binding.BindingGroupName; }
            set { binding.BindingGroupName = value; }
        }

        [DefaultValue(false)]
        public bool BindsDirectlyToSource
        {
            get { return binding.BindsDirectlyToSource; }
            set { binding.BindsDirectlyToSource = value; }
        }

        [DefaultValue(null)]
        public IValueConverter Converter
        {
            get { return binding.Converter; }
            set { binding.Converter = value; }
        }

        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter)), DefaultValue(null)]
        public CultureInfo ConverterCulture
        {
            get { return binding.ConverterCulture; }
            set { binding.ConverterCulture = value; }
        }

        [DefaultValue(null)]
        public object ConverterParameter
        {
            get { return binding.ConverterParameter; }
            set { binding.ConverterParameter = value; }
        }

        [DefaultValue(null)]
        public string ElementName
        {
            get { return binding.ElementName; }
            set { binding.ElementName = value; }
        }

        [DefaultValue(null)]
        public object FallbackValue
        {
            get { return binding.FallbackValue; }
            set { binding.FallbackValue = value; }
        }

        [DefaultValue(false)]
        public bool IsAsync
        {
            get { return binding.IsAsync; }
            set { binding.IsAsync = value; }
        }

        [DefaultValue(BindingMode.Default)]
        public BindingMode Mode
        {
            get { return binding.Mode; }
            set { binding.Mode = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnSourceUpdated
        {
            get { return binding.NotifyOnSourceUpdated; }
            set { binding.NotifyOnSourceUpdated = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnTargetUpdated
        {
            get { return binding.NotifyOnTargetUpdated; }
            set { binding.NotifyOnTargetUpdated = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnValidationError
        {
            get { return binding.NotifyOnValidationError; }
            set { binding.NotifyOnValidationError = value; }
        }

        [DefaultValue(null)]
        public PropertyPath Path
        {
            get { return binding.Path; }
            set { binding.Path = value; }
        }

        [DefaultValue(null)]
        public RelativeSource RelativeSource
        {
            get { return binding.RelativeSource; }
            set { binding.RelativeSource = value; }
        }

        [DefaultValue(null)]
        public object Source
        {
            get { return binding.Source; }
            set { binding.Source = value; }
        }

        [DefaultValue(null)]
        public string StringFormat
        {
            get { return binding.StringFormat; }
            set { binding.StringFormat = value; }
        }

        [DefaultValue(null)]
        public object TargetNullValue
        {
            get { return binding.TargetNullValue; }
            set { binding.TargetNullValue = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter
        {
            get { return binding.UpdateSourceExceptionFilter; }
            set { binding.UpdateSourceExceptionFilter = value; }
        }

        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get { return binding.UpdateSourceTrigger; }
            set { binding.UpdateSourceTrigger = value; }
        }

        [DefaultValue(false)]
        public bool ValidatesOnDataErrors
        {
            get { return binding.ValidatesOnDataErrors; }
            set { binding.ValidatesOnDataErrors = value; }
        }

        [DefaultValue(false)]
        public bool ValidatesOnExceptions
        {
            get { return binding.ValidatesOnExceptions; }
            set { binding.ValidatesOnExceptions = value; }
        }

        [DefaultValue(null)]
        public Collection<ValidationRule> ValidationRules
        {
            get { return binding.ValidationRules; }
        }

        [DefaultValue(null)]
        public string XPath
        {
            get { return binding.XPath; }
            set { binding.XPath = value; }
        }

        #endregion

        private bool IsBindingExpression
        {
            get
            {
                return binding != null &&
                    (binding.Source != null || binding.RelativeSource != null ||
                     binding.ElementName != null || binding.XPath != null ||
                     binding.Path != null);
            }
        }

        /// <summary>
        /// This basic implementation just sets a binding on the targeted
        /// <see cref="DependencyObject"/> and returns the appropriate
        /// <see cref="BindingExpressionBase"/> instance.<br/>
        /// All this work is delegated to the decorated <see cref="Binding"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// In case of a valid binding expression, this is a <see cref="BindingExpressionBase"/>
        /// instance.
        /// </returns>
        /// <param name="provider">Object that can provide services for the markup
        /// extension.</param>
        public override object ProvideValue(IServiceProvider provider)
        {
            //create a binding and associate it with the target
            return binding.ProvideValue(provider);
        }

        protected Binding CreateBinding()
        {
            Binding newBinding = new Binding();
            if (IsBindingExpression)
            {
                // copy all the properties of the binding to the new binding
                //
                if (binding.ElementName != null)
                {
                    newBinding.ElementName = binding.ElementName;
                }
                if (binding.RelativeSource != null)
                {
                    newBinding.RelativeSource = binding.RelativeSource;
                }
                if (binding.Source != null)
                {
                    newBinding.Source = binding.Source;
                }

                newBinding.AsyncState = binding.AsyncState;
                newBinding.BindingGroupName = binding.BindingGroupName;
                newBinding.BindsDirectlyToSource = binding.BindsDirectlyToSource;
                newBinding.Converter = binding.Converter;
                newBinding.ConverterCulture = binding.ConverterCulture;
                newBinding.ConverterParameter = binding.ConverterParameter;
                newBinding.FallbackValue = binding.FallbackValue;
                newBinding.IsAsync = binding.IsAsync;
                newBinding.Mode = binding.Mode;
                newBinding.NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated;
                newBinding.NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated;
                newBinding.NotifyOnValidationError = binding.NotifyOnValidationError;
                newBinding.Path = binding.Path;
                //if (string.IsNullOrEmpty(binding.TargetNullKey))
                //{
                //    newBinding.TargetNullValue = binding.TargetNullValue;
                //}
                //else
                //{
                //    newBinding.TargetNullValue = GetLocalizedResource(bindingTargetNullKey);
                //}
                newBinding.UpdateSourceTrigger = binding.UpdateSourceTrigger;
                newBinding.ValidatesOnDataErrors = binding.ValidatesOnDataErrors;
                newBinding.ValidatesOnExceptions = binding.ValidatesOnExceptions;
                foreach (ValidationRule rule in binding.ValidationRules)
                {
                    newBinding.ValidationRules.Add(rule);
                }
                newBinding.XPath = binding.XPath;
                newBinding.StringFormat = GetStringFormat();
            }

            return newBinding;
        }

        protected abstract string GetStringFormat();

        /// <summary>
        /// Validates a service provider that was submitted to the <see cref="ProvideValue"/>
        /// method. This method checks whether the provider is null (happens at design time),
        /// whether it provides an <see cref="IProvideValueTarget"/> service, and whether
        /// the service's <see cref="IProvideValueTarget.TargetObject"/> and
        /// <see cref="IProvideValueTarget.TargetProperty"/> properties are valid
        /// <see cref="DependencyObject"/> and <see cref="DependencyProperty"/>
        /// instances.
        /// </summary>
        /// <param name="provider">The provider to be validated.</param>
        /// <param name="target">The binding target of the binding.</param>
        /// <param name="dp">The target property of the binding.</param>
        /// <returns>True if the provider supports all that's needed.</returns>
        protected virtual bool TryGetTargetItems(IServiceProvider provider, out DependencyObject target, out DependencyProperty dp)
        {
            target = null;
            dp = null;
            if (provider == null) return false;

            //create a binding and assign it to the target
            IProvideValueTarget service = (IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget));
            if (service == null) return false;

            //we need dependency objects / properties
            target = service.TargetObject as DependencyObject;
            dp = service.TargetProperty as DependencyProperty;
            return target != null && dp != null;
        }
    }
}