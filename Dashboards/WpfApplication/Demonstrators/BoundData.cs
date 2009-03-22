using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication.Demonstrators
{
    /// <summary>
    /// Class to do an example binding
    /// </summary>
    public class BoundObject : INotifyPropertyChanged
    {
        private double _currentValue;
        private double _minValue = 0;
        private double _maxValue = 100;

        #region face text properties
        private Visibility _faceTextVisible;
        private Color _faceTextColor;
        private string _faceTextFormat;

        /// <summary>
        /// Gets or sets the face text format.
        /// </summary>
        /// <value>The face text format.</value>
        public string FaceTextFormat
        {
            get { return _faceTextFormat; }
            set { _faceTextFormat = value; OnPropertyChanged("FaceTextFormat"); }
        }

        /// <summary>
        /// Gets or sets the color of the face text.
        /// </summary>
        /// <value>The color of the face text.</value>
        public Color FaceTextColor
        {
            get { return _faceTextColor; }
            set { _faceTextColor = value; OnPropertyChanged("FaceTextColor"); }
        }

        /// <summary>
        /// Gets or sets the face text visiblity.
        /// </summary>
        /// <value>The face text visiblity.</value>
       public Visibility FaceTextVisibility
        {
            get { return _faceTextVisible; }
            set { _faceTextVisible = value; OnPropertyChanged("FaceTextVisibility"); }
        }
        #endregion


        #region value text properties
        private Visibility _valueTextVisible;
        private Color _valueTextColor;
        private string _valueTextFormat;

        /// <summary>
        /// Gets or sets the value text format.
        /// </summary>
        /// <value>The value text format.</value>
        public string ValueTextFormat
        {
            get { return _valueTextFormat; }
            set { _valueTextFormat = value; OnPropertyChanged("ValueTextFormat"); }
        }

        /// <summary>
        /// Gets or sets the color of the value text.
        /// </summary>
        /// <value>The color of the value text.</value>
        public Color ValueTextColor
        {
            get { return _valueTextColor; }
            set { _valueTextColor = value; OnPropertyChanged("ValueTextColor"); }
        }


        /// <summary>
        /// Gets or sets the value text visible.
        /// </summary>
        /// <value>The value text visible.</value>
        public Visibility ValueTextVisibility
        {
            get { return _valueTextVisible; }
            set { _valueTextVisible = value; OnPropertyChanged("ValueTextVisibility"); }
        }
        #endregion

  

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

        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        /// <value>The min value.</value>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged("MinValue");
            }

        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value.</value>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged("MaxValue");
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
