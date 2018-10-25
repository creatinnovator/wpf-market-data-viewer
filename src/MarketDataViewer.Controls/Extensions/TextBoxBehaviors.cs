using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MarketDataViewer.Controls.Extensions
{
    /// <summary>
    /// Collection of dependency properties and behaviors useful for TextBox
    /// </summary>
    public static class TextBoxBehaviors
    {
        #region FocusedElement

        /// <summary>
        /// Brings focus to the associated TextBox once the text box becomes visible
        /// </summary>
        public static readonly DependencyProperty FocusedElementProperty =
            DependencyProperty.RegisterAttached("FocusedElement", 
                typeof(TextBox), 
                typeof(TextBoxBehaviors),
                new PropertyMetadata(OnFocusedElementChanged));

        public static TextBox GetFocusedElement(DependencyObject element)
        {
            return (TextBox)element.GetValue(FocusedElementProperty);
        }

        public static void SetFocusedElement(DependencyObject element, TextBox value)
        {
            element.SetValue(FocusedElementProperty, value);
        }

        private static void OnFocusedElementChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)obj;

            var oldFocusedElement = (TextBox)e.OldValue;
            if (oldFocusedElement != null)
            {
                element.IsVisibleChanged -= IsVisibleChanged;
            }

            var newFocusedElement = (TextBox)e.NewValue;
            if (newFocusedElement != null)
            {
                element.IsVisibleChanged += IsVisibleChanged;
            }
        }

        private static void IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var focusedElement = GetFocusedElement(element);

            if (focusedElement.Visibility == Visibility.Visible)
            {
                // IsVisibleChanged event is trigged but IsVisible is not yet set to true.
                // Execute the Focus() after the IsVisible flag becomes true
                focusedElement.Dispatcher.BeginInvoke((Action)delegate
                {
                    focusedElement.Focus();
                    Keyboard.Focus(focusedElement);
                }, DispatcherPriority.Render);
            }
        }
        #endregion

        #region IsAlphaNumericOnly

        /// <summary>
        /// Configures TextBox to allow alphanumeric characters only.
        /// Room for improvement: Use regular expression for flexibility which characters will be allowed
        /// by the TextBox
        /// </summary>
        public static readonly DependencyProperty IsAlphaNumericOnlyProperty =
            DependencyProperty.RegisterAttached("IsAlphaNumericOnly",
                typeof(bool),
                typeof(TextBoxBehaviors),
                new PropertyMetadata(OnIsAlphaNumericOnlyChanged));

        public static bool GetIsAlphaNumericOnly(DependencyObject element)
        {
            return (bool)element.GetValue(IsAlphaNumericOnlyProperty);
        }

        public static void SetIsAlphaNumericOnly(DependencyObject element, bool value)
        {
            element.SetValue(IsAlphaNumericOnlyProperty, value);
        }

        private static void OnIsAlphaNumericOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)obj;

            var oldValue = (bool)e.OldValue;
            if (oldValue)
            {
                element.PreviewTextInput -= OnTextInput;
                element.PreviewKeyDown -= OnPreviewKeyDown;
            }

            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                element.PreviewTextInput += OnTextInput;
                element.PreviewKeyDown += OnPreviewKeyDown;
            }
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsInputAccepted(e.Key);
        }

        private static void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsInputAccepted(e.Text);
        }

        public static bool IsInputAccepted(Key key)
        {
            return key != Key.Space;
        }

        public static bool IsInputAccepted(string text)
        {
            return text.All(c => char.IsLetterOrDigit(c));
        }

        #endregion
    }
}
