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
using System.Windows.Threading;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       BoundObject bo = new BoundObject();
        private Random _random = new Random();
        private DispatcherTimer _timer = new DispatcherTimer();

        public Window1()
        {
            InitializeComponent();
            _timer.Interval = new TimeSpan(0,0,1);
            _timer.Tick += new EventHandler(_timer_Elapsed);
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

        void _timer_Elapsed(object sender, EventArgs e)
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

            string t = DateTime.Now.ToString("hhmmsstt");

            _sH1.DisplayCharacter = "" + t[0];
            _sH2.DisplayCharacter = "" + t[1];
            _sM1.DisplayCharacter = "" + t[2];
            _sM2.DisplayCharacter = "" + t[3];
            _sS1.DisplayCharacter = "" + t[4];
            _sS2.DisplayCharacter = "" + t[5];

            _odometer.Increment();
            _mon.Value = _random.Next(100);
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
