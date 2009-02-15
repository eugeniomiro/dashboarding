using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApplication.Demonstrators
{
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


    /// <summary>
    /// Interaction logic for AllControlsDemonstrator.xaml
    /// </summary>
    public partial class AllControlsDemonstrator : UserControl
    {
        BoundObject bo = new BoundObject();
        private Random _random = new Random();
        private DispatcherTimer _timer = new DispatcherTimer();

        public AllControlsDemonstrator()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(2);
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

   
    }
}
