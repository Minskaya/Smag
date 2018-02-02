using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Selection mode enumeration
    /// </summary>
    public enum SelectionMode
    {
        Single,
        Multiple
    }

    /// <summary>
    /// 'Combobox' permettant de
    ///     - grouper sur un niveau
    ///     - filtrer sur une recherche
    ///     - gerer un grand nombre d'élément
    /// </summary>
    [TemplatePart(Name = "PART_ToggleButton", Type = typeof(RadButton))]
    [TemplatePart(Name = "PART_Box", Type = typeof(RadAutoCompleteBox))]
    public class EnhancedSelectionBox : Control
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for GroupingMemberPath.
        /// </summary>
        public static readonly DependencyProperty GroupingMemberPathProperty =
            DependencyProperty.Register(nameof(GroupingMemberPath), typeof(string), typeof(EnhancedSelectionBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Using a DependencyProperty as the backing store for ItemsSource.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(EnhancedSelectionBox), new PropertyMetadata((Enumerable.Empty<object>())));

        /// <summary>
        /// Using a DependencyProperty as the backing store for ItemTemplate.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EnhancedSelectionBox), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for SelectedItem.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EnhancedSelectionBox));

        /// <summary>
        /// Using a DependencyProperty as the backing store for SelectedItems.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(IEnumerable), typeof(EnhancedSelectionBox));

        /// <summary>
        /// Using a DependencyProperty as the backing store for SelectedItemTemplate.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register(nameof(SelectedItemTemplate), typeof(DataTemplate), typeof(EnhancedSelectionBox), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for SelectionMode.
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register(nameof(SelectionMode), typeof(SelectionMode), typeof(EnhancedSelectionBox), new PropertyMetadata(Poc_ComboPlus.SelectionMode.Single));

        /// <summary>
        /// Using a DependencyProperty as the backing store for TextSearchPath.
        /// </summary>
        public static readonly DependencyProperty TextSearchPathProperty =
            DependencyProperty.Register(nameof(TextSearchPath), typeof(string), typeof(EnhancedSelectionBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Constructeur statique
        /// </summary>
        static EnhancedSelectionBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnhancedSelectionBox), new FrameworkPropertyMetadata(typeof(EnhancedSelectionBox)));
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public EnhancedSelectionBox()
        {
            _sort = new SortDescriptionCollection();
        }

        /// <summary>
        /// Nom de la propriété sur laquelle on groupe
        /// </summary>
        public string GroupingMemberPath
        {
            get { return (string)GetValue(GroupingMemberPathProperty); }
            set { SetValue(GroupingMemberPathProperty, value); }
        }

        /// <summary>
        /// Source des données
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Template des éléments dans la liste de suggestion
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Element sélectionné
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Liste des éléments sélectionnés quand SelectionMode = Multiple
        /// </summary>
        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>
        /// Template des élements sélectionnés quand SelectionMode = Multiple
        /// </summary>
        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }

        /// <summary>
        /// Mode de sélection
        /// </summary>
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        /// <summary>
        /// Collection de SortDescriptions pour trier les regroupements.
        /// </summary>
        public SortDescriptionCollection SortDescriptions
        {
            get { return _sort; }
        }

        /// <summary>
        /// Nom de la propriété sur laquelle s'effectue la recherche
        /// </summary>
        public string TextSearchPath
        {
            get { return (string)GetValue(TextSearchPathProperty); }
            set { SetValue(TextSearchPathProperty, value); }
        }

        /// <summary>
        /// Appellé lors de l'application du template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _part_ToggleButton = GetTemplateChild("PART_ToggleButton") as RadButton;
            _part_Box = GetTemplateChild("PART_Box") as RadAutoCompleteBox;

            if (_part_ToggleButton == null || _part_Box == null)
            {
                throw new NullReferenceException("Template parts not available");
            }

            _part_ToggleButton.Click += Part_ToggleButton_Click;
        }

        /// <summary>
        /// Backing field pour les tris
        /// </summary>
        private readonly SortDescriptionCollection _sort;

        /// <summary>
        /// RadAutoCompleteBox utilisée pour la saisie
        /// </summary>
        private RadAutoCompleteBox _part_Box;

        /// <summary>
        /// Bouton pour déplier la pseudo combo
        /// </summary>
        private RadButton _part_ToggleButton;

        /// <summary>
        /// Handler de l'évènement click sur le bouton
        /// </summary>
        private void Part_ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _part_Box.Focus();
            _part_Box.Populate(_part_Box.SearchText);
        }
    }
}