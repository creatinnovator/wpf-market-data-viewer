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

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z ||
                e.Key >= Key.D0 && e.Key <= Key.D9 ||
                e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                // When A-Z, 0-9 is keyed in, we show the element attached in this property
                var element = (FrameworkElement)sender;
                var elementToShow = GetElementToShow(element);
                if (elementToShow != null)
                {
                    elementToShow.Visibility = Visibility.Visible;
                    e.Handled = false;
                }
            }
            else if (e.Key == Key.Escape)
            {
                // When ESC is keyed in, we hide the element attached in this property
                var element = (FrameworkElement)sender;
                var elementToShow = GetElementToShow(element);
                if (elementToShow != null)
                {
                    elementToShow.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
