using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApplication.Demonstrators
{
    /// <summary>
    /// Class to do an example binding
    /// </summary>
    public class BoundObject : INotifyPropertyChanged
    {
        private double _currentValue;


        /// <summary>
        /// Current vlaue property, raises the on property changed event
        /// </summary>
        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }

        

        #region INotifyPropertyChanged Members

        /// <summary>
        /// One of my properties has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify any listeners that a property has changed
        /// </summary>
        /// <param name="propName">Name of the property</param>
        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
