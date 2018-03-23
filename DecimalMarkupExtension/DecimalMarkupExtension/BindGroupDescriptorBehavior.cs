using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace DecimalMarkupExtension
{
    public class BindGroupDescriptorBehavior : Behavior<RadGridView>
    {
        // Using a DependencyProperty as the backing store for GroupDescriptor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupDescriptorProperty =
            DependencyProperty.Register("GroupDescriptor", typeof(GroupDescriptor), typeof(BindGroupDescriptorBehavior), new PropertyMetadata(null));

        public GroupDescriptor GroupDescriptor
        {
            get { return (GroupDescriptor)GetValue(GroupDescriptorProperty); }
            set { SetValue(GroupDescriptorProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }
    }
}