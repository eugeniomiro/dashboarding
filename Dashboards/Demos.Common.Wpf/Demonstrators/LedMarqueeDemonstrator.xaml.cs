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

    public class MarqueDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Scrolling LED Marquee"; }
        }

        public UserControl Create()
        {
            return new LedMarqueeDemonstrator();
        }

        #endregion
    }

    /// <summary>
    /// Interaction logic for LedMarqueeDemonstrator.xaml
    /// </summary>
    public partial class LedMarqueeDemonstrator : UserControl
    {
        public LedMarqueeDemonstrator()
        {
            InitializeComponent();
        }
    }
}
