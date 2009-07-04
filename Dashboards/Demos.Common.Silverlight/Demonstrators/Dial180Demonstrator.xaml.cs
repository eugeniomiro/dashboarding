using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Codeplex.Dashboarding;

namespace Demos.Common.Demonstrators
{
    
    public class Dial180Demo: IDemonstrateDials
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
