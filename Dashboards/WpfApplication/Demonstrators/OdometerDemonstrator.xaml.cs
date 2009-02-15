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
    class OdoDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Odometer"; }
        }

        public UserControl Create()
        {
            return new OdometerDemonstrator();
        }

        #endregion
    }

    /// <summary>
    /// Interaction logic for OdometerDemonstrator.xaml
    /// </summary>
    public partial class OdometerDemonstrator : UserControl
    {
        public OdometerDemonstrator()
        {
            InitializeComponent();
        }
    }
}
