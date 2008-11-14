using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication.SingleGuages
{
    public partial class FourthStep : UserControl
    {
        public FourthStep()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(IAmLoaded);
        }

        void IAmLoaded(object sender, RoutedEventArgs e)
        {

            Car porsche = new Car { MilesPerHour = 99 };
            _dial.DataContext = porsche;
        }
    }

    public class Car
    {
        public double MilesPerHour { get; set; }
    }

}
