using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Timers;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       BoundObject bo = new BoundObject();
        private Random _random = new Random();
        private System.Timers.Timer _timer = new System.Timers.Timer();

        public Window1()
        {
            InitializeComponent();
            _timer.Interval = 1000;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            Loaded += new RoutedEventHandler(IAmLoaded);
            bo = new BoundObject { CurrentValue = 0 };
            DataContext = bo;
            _timer.Start();
        }


        /// <summary>
        /// On load show value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IAmLoaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetValues();
        }

        /// <summary>
        /// Set the value and display
        /// </summary>
        private void SetValues()
        {
          
            bo.CurrentValue = _random.Next(100); 
            bo.CurrentValue2 = _random.Next(100); 
            bo.CurrentValue3 = _random.Next(100); 
            bo.CurrentValue4 = _random.Next(100);

        }

    }

    #region data bound class
    /// <summary>
    /// Class to do an example binding
    /// </summary>
    public class BoundObject : INotifyPropertyChanged
    {
        private double _currentValue;
        private double _currentValue2;
        private double _currentValue3;
        private double _currentValue4;

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
        /// Current vlaue property, raises the on property changed event
        /// </summary>
        public double CurrentValue2
        {
            get { return _currentValue2; }
            set
            {
                _currentValue2 = value;
                OnPropertyChanged("CurrentValue2");
            }
        }
        /// <summary>
        /// Current vlaue property, raises the on property changed event
        /// </summary>
        public double CurrentValue3
        {
            get { return _currentValue3; }
            set
            {
                _currentValue3 = value;
                OnPropertyChanged("CurrentValue3");
            }
        }
        /// <summary>
        /// Current vlaue property, raises the on property changed event
        /// </summary>
        public double CurrentValue4
        {
            get { return _currentValue4; }
            set
            {
                _currentValue4 = value;
                OnPropertyChanged("CurrentValue4");
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
    #endregion
    }
}
