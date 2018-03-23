using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace DecimalMarkupExtension
{
    public class BindGroupDescriptorBehavior : Behavior<RadGridView>
    {
        // Using a DependencyProperty as the backing store for GroupDescriptor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupDescriptorProperty =
            DependencyProperty.Register(
                "GroupDescriptor",
                typeof(ColumnGroupDescriptor),
                typeof(BindGroupDescriptorBehavior),
                new PropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnGroupDescriptorsPropertyChanged)));

        public ColumnGroupDescriptor GroupDescriptor
        {
            get { return (ColumnGroupDescriptor)GetValue(GroupDescriptorProperty); }
            set { SetValue(GroupDescriptorProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        private static void OnGroupDescriptorsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var instance = dependencyObject as BindGroupDescriptorBehavior;
            if (instance != null)
            {
                instance.UpdateGroupDescriptor();
            }
        }

        private void UpdateGroupDescriptor()
        {
            this.AssociatedObject.GroupDescriptors.Clear();
            this.AssociatedObject.GroupDescriptors.Add(this.GroupDescriptor);
        }
    }
}