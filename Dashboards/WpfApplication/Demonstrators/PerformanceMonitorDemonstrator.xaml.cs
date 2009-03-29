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
  
    /// <summary>
    /// Interaction logic for PerformanceMonitorDemonstrator.xaml
    /// </summary>
    public partial class PerformanceMonitorDemonstrator : UserControl
    {
        public PerformanceMonitorDemonstrator()
        {
            InitializeComponent();
            DataContext = new BoundObject() ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _l.Value = 77;
        }
    }

    class PerfDemo : IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Performance Monitor"; }
        }

        public UserControl Create()
        {
            return new PerformanceMonitorDemonstrator();
        }

        #endregion
    }
}
