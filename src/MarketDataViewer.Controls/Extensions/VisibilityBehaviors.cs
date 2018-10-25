using MarketDataViewer.Controls.Views;
using System.Windows;
using System.Windows.Input;

namespace MarketDataViewer.Controls.Extensions
{
    /// <summary>
    /// Visibility relted behaviors/helpers
    /// </summary>
    public static class VisibilityBehaviors
    {
        /// <summary>
        /// Controls visibility of the specified element/control when a keystroke is 
        /// detected on the element where this property is attached
        /// </summary>
        public static readonly DependencyProperty ShowWhenKeystrokeProperty =
            DependencyProperty.RegisterAttached("ShowWhenKeystroke",
                typeof(IAddSymbolView),
                typeof(VisibilityBehaviors),
                new PropertyMetadata(OnShowWhenKeystrokeChanged));

        public static IAddSymbolView GetShowWhenKeystroke(DependencyObject element)
        {
            return (IAddSymbolView)element.GetValue(ShowWhenKeystrokeProperty);
        }

        public static void SetShowWhenKeystroke(DependencyObject element, IAddSymbolView value)
        {
            element.SetValue(ShowWhenKeystrokeProperty, value);
        }

        private static void OnShowWhenKeystrokeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)obj;

            var oldElement = (IAddSymbolView)e.OldValue;
            if (oldElement != null)
            {
                element.PreviewKeyDown -= OnPreviewKeyDown;
            }

            var newElement = (IAddSymbolView)e.NewValue;
            if (newElement != null)
            {
                element.PreviewKeyDown += OnPreviewKeyDown;
            }
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e) => 
            DoShowWhenKeystroke(
                GetShowWhenKeystroke((DependencyObject)sender) as IAddSymbolView, 
                e.Key);

        public static void DoShowWhenKeystroke(IAddSymbolView viewToShow, Key key)
        {
            if (key >= Key.A && key <= Key.Z ||
                key >= Key.D0 && key <= Key.D9 ||
                key >= Key.NumPad0 && key <= Key.NumPad9)
            {
                // When A-Z, 0-9 is keyed in, we show the element attached in this property
                if (viewToShow != null)
                {
                    if (viewToShow.Visibility != Visibility.Visible)
                    {
                        viewToShow.Visibility = Visibility.Visible;
                        viewToShow.SetSymbol(ConvertToChar(key));
                    }
                }
            }
            else if (key == Key.Escape)
            {
                // When ESC is keyed in, we hide the element attached in this property
                if (viewToShow != null)
                {
                    viewToShow.Visibility = Visibility.Collapsed;
                }
            }
        }

        public static string ConvertToChar(Key key)
        {
            if (key >= Key.D0 && key <= Key.D9)
            {
                return ((char)(key - Key.D0 + '0')).ToString();
            }
            if (key >= Key.NumPad0 && key <= Key.NumPad9)
            {
                return ((char)(key - Key.NumPad0 + '0')).ToString();
            }
            if (key >= Key.A && key <= Key.Z)
            {
                return ((char)(key - Key.A + 'A')).ToString();
            }
            return "";
        }
    }
}
