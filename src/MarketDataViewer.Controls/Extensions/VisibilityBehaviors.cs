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
        public static readonly DependencyProperty ElementToShowProperty =
            DependencyProperty.RegisterAttached("ElementToShow",
                typeof(FrameworkElement),
                typeof(VisibilityBehaviors),
                new PropertyMetadata(OnElementToShowChanged));

        public static FrameworkElement GetElementToShow(DependencyObject element)
        {
            return (FrameworkElement)element.GetValue(ElementToShowProperty);
        }

        public static void SetElementToShow(DependencyObject element, FrameworkElement value)
        {
            element.SetValue(ElementToShowProperty, value);
        }

        private static void OnElementToShowChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)obj;

            var oldElement = (FrameworkElement)e.OldValue;
            if (oldElement != null)
            {
                element.PreviewKeyDown -= OnPreviewKeyDown;
            }

            var newElement = (FrameworkElement)e.NewValue;
            if (newElement != null)
            {
                element.PreviewKeyDown += OnPreviewKeyDown;
            }
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e) => 
            HandleKeyDown(GetElementToShow((DependencyObject)sender), e.Key);

        public static void HandleKeyDown(FrameworkElement elementToShow, Key key)
        {
            if (key >= Key.A && key <= Key.Z ||
                key >= Key.D0 && key <= Key.D9 ||
                key >= Key.NumPad0 && key <= Key.NumPad9)
            {
                // When A-Z, 0-9 is keyed in, we show the element attached in this property
                if (elementToShow != null)
                {
                    elementToShow.Visibility = Visibility.Visible;
                }
            }
            else if (key == Key.Escape)
            {
                // When ESC is keyed in, we hide the element attached in this property
                if (elementToShow != null)
                {
                    elementToShow.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
