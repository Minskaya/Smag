using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
    public class EnhancedComboBox : Control
    {
        // Using a DependencyProperty as the backing store for GroupingMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupingMemberPathProperty =
            DependencyProperty.Register(nameof(GroupingMemberPath), typeof(string), typeof(EnhancedComboBox), new PropertyMetadata(string.Empty));

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(EnhancedComboBox), new PropertyMetadata((Enumerable.Empty<object>())));

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EnhancedComboBox), null);

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EnhancedComboBox));

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(IEnumerable), typeof(EnhancedComboBox));

        // Using a DependencyProperty as the backing store for TextSearchPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSearchPathProperty =
            DependencyProperty.Register(nameof(TextSearchPath), typeof(string), typeof(EnhancedComboBox), new PropertyMetadata(string.Empty));

        static EnhancedComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnhancedComboBox), new FrameworkPropertyMetadata(typeof(EnhancedComboBox)));
        }

        public string GroupingMemberPath
        {
            get { return (string)GetValue(GroupingMemberPathProperty); }
            set { SetValue(GroupingMemberPathProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public string TextSearchPath
        {
            get { return (string)GetValue(TextSearchPathProperty); }
            set { SetValue(TextSearchPathProperty, value); }
        }
    }
}