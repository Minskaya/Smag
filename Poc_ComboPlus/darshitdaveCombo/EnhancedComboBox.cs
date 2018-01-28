using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace darshitdaveCombo
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:darshitdaveCombo"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:darshitdaveCombo;assembly=darshitdaveCombo"
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
    public class EnhancedComboBox : ComboBox
    {
        // Using a DependencyProperty as the backing store for CollapsedItemNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollapsedItemNumberProperty =
            DependencyProperty.Register(nameof(CollapsedItemNumber), typeof(int), typeof(EnhancedComboBox), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for GroupingMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupingMemberPathProperty =
            DependencyProperty.Register(nameof(GroupingMemberPath), typeof(string), typeof(EnhancedComboBox), new PropertyMetadata(string.Empty, OnGroupMemberPathChanged));

        // Using a DependencyProperty as the backing store for dataTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(EnhancedComboBox), new PropertyMetadata(default(GroupStyle), OnHeaderTemplateChanged));

        private CollectionViewSource cvs = new CollectionViewSource();

        static EnhancedComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnhancedComboBox), new FrameworkPropertyMetadata(typeof(EnhancedComboBox)));
        }

        public EnhancedComboBox()
        {
        }

        public int CollapsedItemNumber
        {
            get { return (int)GetValue(CollapsedItemNumberProperty); }
            set { SetValue(CollapsedItemNumberProperty, value); }
        }

        public string GroupingMemberPath
        {
            get { return (string)GetValue(GroupingMemberPathProperty); }
            set { SetValue(GroupingMemberPathProperty, value); }
        }

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    var cmb = (ComboBox)this.GetTemplateChild("cmb");
        //    cmb.ItemsSource = cvs.View;
        //}

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            var cvs = (CollectionViewSource)this.FindResource("GroupedData1");
            cvs.Source = newValue;

            //cvs.Source = newValue;
            //cvs.GroupDescriptions.Add(new PropertyGroupDescription()
            //{
            //    PropertyName = "GroupName"
            //});
        }

        private static void OnGroupMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = (EnhancedComboBox)d;
            var cvs = (CollectionViewSource)comboBox.FindResource("GroupedData1");
            //((PropertyGroupDescription)cvs.GroupDescriptions
            //    .First())
            //    .PropertyName = (string)e.NewValue;
        }

        private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = (EnhancedComboBox)d;
            var groupStyle = (GroupStyle)comboBox.FindResource("MyGroupStyle");
            groupStyle.HeaderTemplate = (DataTemplate)e.NewValue;

            comboBox.GroupStyle.Clear();
            comboBox.GroupStyle.Add(groupStyle);
        }
    }
}