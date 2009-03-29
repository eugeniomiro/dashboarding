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

namespace WpfApplication.Demonstrators
{
    class Dial90Demo : IDemonstrateDials
    {


        public string DemoName
        {
            get { return "Dial90"; }
        }


        public UserControl Create()
        {
            return new Dial90Demonstrator();
        }


    }

    /// <summary>
    /// Interaction logic for Dial90Demonstrator.xaml
    /// </summary>
    public partial class Dial90Demonstrator : UserControl
    {
        private BoundObject _object = new BoundObject
        {
            CurrentValue = 50,
            FaceTextColor = Colors.Black,
            FaceTextFormat = "{0:0}",
            FaceTextVisibility = Visibility.Visible,
            ValueTextColor = Colors.Black,
            ValueTextFormat = "{0:0}",
            ValueTextVisibility = Visibility.Visible,
        };

        public Dial90Demonstrator()
        {
            InitializeComponent();
            DataContext = _object;
        }

    }
}
