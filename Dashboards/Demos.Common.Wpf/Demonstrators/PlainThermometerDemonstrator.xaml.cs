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
    public class PlainThermometerDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Thermometers"; }
        }

        public UserControl Create()
        {
            return new PlainThermometerDemonstrator();
        }

        #endregion
    }


    /// <summary>
    /// Interaction logic for PlainThermometerDemonstrator.xaml
    /// </summary>
    public partial class PlainThermometerDemonstrator : UserControl
    {
        private BoundObject _object = new BoundObject { CurrentValue = 50, FaceTextColor = Colors.Wheat, ValueTextColor = Colors.AliceBlue };

        public PlainThermometerDemonstrator()
        {
            InitializeComponent();
            DataContext = _object;
        }
    }
}
