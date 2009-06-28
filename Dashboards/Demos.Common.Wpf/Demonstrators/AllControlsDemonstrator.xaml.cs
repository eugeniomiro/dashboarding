using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using Codeplex.Dashboarding;

namespace Demos.Common.Demonstrators
{
   

    /// <summary>
    /// Interaction logic for AllControlsDemonstrator.xaml
    /// </summary>
    public partial class AllControlsDemonstrator : UserControl
    {
        BoundObject bo = new BoundObject();
        private Random _random = new Random();
        private DispatcherTimer _timer = new DispatcherTimer();


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
            bo = new BoundObject
            {
                CurrentValue = 50,
                FaceTextColor = Colors.White,
                FaceTextFormat = "{0:0}",
                FaceTextVisibility = Visibility.Visible,
                ValueTextColor = Colors.White,
                ValueTextFormat = "{0:0}",
                ValueTextVisibility = Visibility.Visible,
                FaceColorRange = new ColorPointCollection(),
                NeedleColorRange = new ColorPointCollection()
            };
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

        private Random random = new Random();
     


    }



    public class AllControlsDemo : IDemonstrateDials
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
