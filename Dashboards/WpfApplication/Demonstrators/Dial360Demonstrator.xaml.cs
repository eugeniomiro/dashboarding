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
    class Dial360Demo : IDemonstrateDials
    {

      
        public string DemoName
        {
            get { return "Dial360"; }
        }


        public UserControl Create()
        {
            return new Dial360Demonstrator();
        }

   
    }

    /// <summary>
    /// Interaction logic for Dial180.xaml
    /// </summary>
    public partial class Dial360Demonstrator : UserControl
    {
        private BoundObject _object = new BoundObject { CurrentValue = 50 };
        public Dial360Demonstrator()
        {
            InitializeComponent();
            DataContext = _object;
        }




    }
}
