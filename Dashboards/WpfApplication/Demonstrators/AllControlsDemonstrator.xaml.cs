using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApplication.Demonstrators
{
   

    /// <summary>
    /// Interaction logic for AllControlsDemonstrator.xaml
    /// </summary>
    public partial class AllControlsDemonstrator : UserControl
    {
        BoundObject bo = new BoundObject();
        private Random _random = new Random();
        private DispatcherTimer _timer = new DispatcherTimer();
        private Foo foo = new Foo();

        private List<Color> randomColors = new List<Color>() {
            Colors.Blue, 
            Colors.Red, 
            Colors.Yellow, 
            Colors.Goldenrod, 
            Colors.Green, 
            Colors.Lavender,
            Colors.Black,
            Colors.Chocolate,
            Colors.White,
            Colors.Tomato,
            Colors.SteelBlue,
        };

        public AllControlsDemonstrator()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += new EventHandler(_timer_Elapsed);
            Loaded += new RoutedEventHandler(IAmLoaded);
            bo = new BoundObject { CurrentValue = 0 };
            DataContext = bo;
            _timer.Start();
            DataContext = this.foo;
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

       
        public string DemoName
        {
            get { return "All Controls"; }
        }

        private Random random = new Random();
        private void _cb_Checked(object sender, RoutedEventArgs e)
        {
            this.foo.TextVisibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var col = randomColors[this.random.Next(randomColors.Count)];
            this.foo.TextColor = col;
        }

        private void _cb_Unchecked(object sender, RoutedEventArgs e)
        {
            this.foo.TextVisibility = Visibility.Collapsed;
        }

      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foo.Format = _format.Text;
        }


        private void Button_Click_FFormat(object sender, RoutedEventArgs e)
        {
            foo.FaceFormat = _txtFF.Text;
        }

        private void _cbf_Checked(object sender, RoutedEventArgs e)
        {
            this.foo.FaceTextVisibility = Visibility.Visible;
        }
        private void _cbf_Unchecked(object sender, RoutedEventArgs e)
        {
            this.foo.FaceTextVisibility = Visibility.Collapsed;
        }

        private void Button_ClickFColor(object sender, RoutedEventArgs e)
        {
            var col = randomColors[this.random.Next(randomColors.Count)];
            this.foo.FaceTextColor = col;

        }
   
    }

    class Foo : INotifyPropertyChanged
    {
        public Foo()
        {
            FaceTextVisibility = Visibility.Visible;
            TextVisibility = Visibility.Visible;
            TextColor = Colors.Black;
            Format = "{0:000}";
            FaceFormat = "{0:000}";
            FaceTextColor = Colors.BlanchedAlmond;
        }

        private Visibility textVisibility;

        public Visibility TextVisibility
        {
            get { return textVisibility; }
            set { textVisibility = value; OnPropertyChanged("TextVisibility"); }
        }

        private Visibility faceTextVisibility;
        public Visibility FaceTextVisibility                         
        {
            get { return faceTextVisibility; }
            set { faceTextVisibility = value; OnPropertyChanged("FaceTextVisibility"); }
        }

        private Color _textColor;

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; OnPropertyChanged("TextColor"); OnPropertyChanged("AsBrush"); }
        }

        private Color _ftextColor;

        public Color FaceTextColor
        {
            get { return _ftextColor; }
            set { _ftextColor = value; OnPropertyChanged("FaceTextColor"); OnPropertyChanged("FaceAsBrush"); }
        }


        private string _format;

        public string Format
        {
            get { return _format; }
            set { _format = value; OnPropertyChanged("Format"); }
        }

        private string _fformat;

        public string FaceFormat
        {
            get { return _fformat; }
            set { _fformat = value; OnPropertyChanged("FaceFormat"); }
        }

        public Brush AsBrush { get { return new SolidColorBrush(TextColor); } }
        public Brush FaceAsBrush { get { return new SolidColorBrush(FaceTextColor); } }

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


    class AllControlsDemo : IDemonstrateDials
    {


        public string DemoName
        {
            get { return "All controls"; }
        }


        public UserControl Create()
        {
            return new AllControlsDemonstrator();
        }


    }

}
