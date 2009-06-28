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

namespace Demos.Common.Demonstrators
{
    public class VduDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Decade VU Meter"; }
        }

        public UserControl Create()
        {
            return new VduMeterDemonstrator();
        }

        #endregion
    }
    /// <summary>
    /// Interaction logic for VduMeterDemonstrator.xaml
    /// </summary>
    public partial class VduMeterDemonstrator : UserControl
    {
        public VduMeterDemonstrator()
        {
            InitializeComponent();
            this.DataContext = new BoundObject();
        }
    }
}
