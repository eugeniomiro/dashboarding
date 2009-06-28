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
    public class ProgressbarDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Progress bars"; }
        }

        public UserControl Create()
        {
            return new ProgressbarDemonstrator();
        }

        #endregion
    }
    /// <summary>
    /// Interaction logic for ProgressbarDemonstrator.xaml
    /// </summary>
    public partial class ProgressbarDemonstrator : UserControl
    {
        private BoundObject _object = new BoundObject { CurrentValue = 50 };

        public ProgressbarDemonstrator()
        {
            InitializeComponent();
            DataContext = _object;
        }
    }
}
