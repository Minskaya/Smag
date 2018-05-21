using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Smag.Tars.UI.Wpf.Controls
{
    /// <summary>
    /// Représente une textbox qui s'agrandit lors de l'edition
    /// </summary>
    [Description("Représente une textbox qui s'agrandit lors de l'edition")]
    [TemplatePart(Name = PART_SummaryTextbox, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Popup, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_FullTextBox, Type = typeof(FrameworkElement))]
    public class ExpandingTextBox : Control
    {
        #region Public Fields

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(ExpandingTextBox),
                new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTextChanged)));

        /// <summary>
        /// Hauteur de la popup
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "DependencyProperty")]
        public static DependencyProperty PopupHeightProperty;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Taille par défaut du résumé
        /// </summary>
        private const int DefaultSummaryLength = 15;

        /// <summary>
        /// Part textbox d'edition
        /// </summary>
        private const string PART_FullTextBox = "PART_FullTextBox";

        /// <summary>
        /// Part popup
        /// </summary>
        private const string PART_Popup = "PART_Popup";

        /// <summary>
        /// Part résumé
        /// </summary>
        private const string PART_SummaryTextbox = "PART_SummaryTextbox";

        /// <summary>
        /// Clé d'accé à la propriété résumé
        /// </summary>
        //private static DependencyPropertyKey SummaryTextPropertyKey;

        /// <summary>
        /// Textbox contenant le texte complet
        /// </summary>
        private TextBox _fullTextbox = null;

        /// <summary>
        /// Popup d'édition
        /// </summary>
        private Popup _popup = null;

        /// <summary>
        /// Textbox du résumé
        /// </summary>
        private TextBox _summaryTextbox = null;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructeur statique
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Le constructeur ne peut etre supprimé.")]
        static ExpandingTextBox()
        {
            // Override style
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ExpandingTextBox),
                new FrameworkPropertyMetadata(typeof(ExpandingTextBox)));

            PopupHeightProperty = DependencyProperty.Register(
                nameof(PopupHeight),
                typeof(int),
                typeof(ExpandingTextBox),
                new FrameworkPropertyMetadata(300));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Taille de la popup d'édition
        /// </summary>
        public int PopupHeight
        {
            get
            {
                return (int)GetValue(PopupHeightProperty);
            }

            set
            {
                SetValue(PopupHeightProperty, value);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Abonne les evenements lors de l'application du template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            if (_summaryTextbox != null)
            {
                _summaryTextbox.GotFocus -= _summaryTextbox_GotFocus;
                _summaryTextbox = null;
            }

            //if (_fullTextbox != null)
            //{
            //    _fullTextbox.PreviewKeyDown -= _fullTextbox_PreviewKeyDown;
            //    _fullTextbox = null;
            //}

            _summaryTextbox = (TextBox)Template.FindName(PART_SummaryTextbox, this);
            if (_summaryTextbox != null)
            {
                _summaryTextbox.GotFocus += _summaryTextbox_GotFocus;
                _summaryTextbox.Text = SummarizeText(this.Text);
            }

            _popup = (Popup)Template.FindName(PART_Popup, this);
            if (_popup != null)
            {
                _popup.Closed += _popup_Closed;
            }

            _fullTextbox = (TextBox)Template.FindName(PART_FullTextBox, this);
            if (_fullTextbox != null)
            {
                //    _fullTextbox.PreviewKeyDown += _fullTextbox_PreviewKeyDown;
                _fullTextbox.LostKeyboardFocus += _fullTextbox_LostKeyboardFocus;
                _fullTextbox.LostFocus += _fullTextbox_LostFocus;
            }
        }

        private void _fullTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("_fullTextbox_LostFocus");
        }

        private void _fullTextbox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("_fullTextbox_LostKeyboardFocus");
        }

        private void ExpandingTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _popup.IsOpen = false;
        }

            #endregion Public Methods

            #region Private Methods

            /// <summary>
            /// Handler quand Text change
            /// </summary>
            private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ExpandingTextBox tx = d as ExpandingTextBox;

                if (tx._summaryTextbox != null)
                {
                    tx._summaryTextbox.Text = SummarizeText(e.NewValue); ;
                }
            }

            private static string SummarizeText(object value)
            {
                var text = (string)value;

                // on supprimme tous les sauts de lignes
                var trimmed = string.IsNullOrEmpty(text)
                              ? string.Empty
                              : text
                                    .Replace(Environment.NewLine, " ")
                                    .Trim();
                return trimmed;
            }

            /// <summary>
            /// Ferme la popup lorsque l'utilisateur appuie sur la tabulation
            /// </summary>
            private void _fullTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Tab)
                {
                    _popup.IsOpen = false;
                    this.MoveToNextUIElement();
                    e.Handled = true;
                }
            }

            private void _popup_Closed(object sender, EventArgs e)
            {
                this.MoveToNextUIElement();
            }

            /// <summary>
            /// Ouvre la popup quand la textbox obtient le focus
            /// </summary>
            private void _summaryTextbox_GotFocus(object sender, RoutedEventArgs e)
            {
                _summaryTextbox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _popup.IsOpen = true;
                    _fullTextbox.Focus();
                }));

                e.Handled = true;
            }


        /// <summary>
        /// Handler quand le texte change
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Controls.TextBox.set_Text(System.String)", Justification = "this is a user message.")]
        private void ExpandingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_summaryTextbox != null)
            {
                // on supprimme tous les sauts de lignes
                var trimmed = string.IsNullOrEmpty(this.Text)
                              ? string.Empty
                              : this.Text
                                    .Replace(Environment.NewLine, " ")
                                    .Trim();

                _summaryTextbox.Text = trimmed;
            }
        }

        /// <summary>
        /// Navigue sur le champ suivant quand on quitte l'edition
        /// </summary>
        private void MoveToNextUIElement()
        {
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);

            // Change keyboard focus.
            if (_summaryTextbox != null)
            {
                _summaryTextbox.MoveFocus(request);
            }
        }

        #endregion Private Methods
    }
}