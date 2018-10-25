using Prism.Mvvm;
using System;

namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// Base view model, as we might need more functionality other what 
    /// Prism's BindableBase current have.
    /// </summary>
    public abstract class ViewModelBase : BindableBase
    {
        /// <summary>
        /// Provides an easy way to add property change handler to monitor property changes
        /// within our VMs
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="action"></param>
        public void AddPropertyChangedHandler(string propertyName, Action action)
        {
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    action();
                }
            };
        }
    }
}
