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
using Codeplex.Dashboarding;

namespace WpfApplication.Demonstrators
{
    
    class Dial180Demo: IDemonstrateDials
    {

        #region IDemonstrateDials Members

        public string DemoName
        {
            get { return "Dial180"; }
        }

        public UserControl Create()
        {
            return new Dial180Demonstrator();
        }

        #endregion
    }


    /// <summary>
    /// Interaction logic for Dial180Demonstrator.xaml
    /// </summary>
    public partial class Dial180Demonstrator : UserControl
    {

        private BoundObject _object = new BoundObject
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


        public Dial180Demonstrator()
        {
            InitializeComponent();
            DataContext = _object;
        }

        #region IDemonstrateDials Members

      
        #endregion
    }
}
