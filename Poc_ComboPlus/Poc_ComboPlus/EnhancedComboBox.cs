using System.Windows;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Poc_ComboPlus"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Poc_ComboPlus;assembly=Poc_ComboPlus"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:EnhancedComboBox/>
    ///
    /// </summary>
    public class EnhancedComboBox : RadComboBox
    {
        // Using a DependencyProperty as the backing store for GroupingMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupingMemberPathProperty =
            DependencyProperty.Register(nameof(GroupingMemberPath), typeof(string), typeof(EnhancedComboBox), new PropertyMetadata(string.Empty, OnGroupMemberPathChanged));

        // Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(EnhancedComboBox), new PropertyMetadata(0));

        static EnhancedComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnhancedComboBox), new FrameworkPropertyMetadata(typeof(EnhancedComboBox)));
        }

        public string GroupingMemberPath
        {
            get { return (string)GetValue(GroupingMemberPathProperty); }
            set { SetValue(GroupingMemberPathProperty, value); }
        }

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        private static void OnGroupMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = (EnhancedComboBox)d;
            //((PropertyGroupDescription)cvs.GroupDescriptions
            //    .First())
            //    .PropertyName = (string)e.NewValue;
        }
    }
}