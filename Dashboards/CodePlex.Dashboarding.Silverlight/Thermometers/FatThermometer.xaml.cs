using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Thermometers
{
    public class FatThermometer : WebServiceEnabledDashboard
    {
        public FatThermometer(string manifest): base(manifest)
        {

        }

        public FatThermometer(): base("CodePlex.Dashboarding.Silverlight.Thermometers.FatThermometer.xaml")
        {

        }

        public string Text
        {
            get
            {
                TextBlock tb = Root.FindName("_message") as TextBlock;
                return tb.Text;
            }
            set
            {
                TextBlock tb = Root.FindName("_message") as TextBlock;
                if (tb != null)
                {
                    tb.Text = value;
                }
            }
        }

        public string LowerBoundText
        {
            get
            {
                TextBlock tb = Root.FindName("_lowerBoundText") as TextBlock;
                return tb.Text;
            }
            set
            {
                TextBlock tb = Root.FindName("_lowerBoundText") as TextBlock;
                tb.Text = value;
            }
        }

        public string UpperBoundText
        {
            get
            {
                TextBlock tb = Root.FindName("_upperBoundText") as TextBlock;
                return tb.Text;
            }
            set
            {
                TextBlock tb = Root.FindName("_upperBoundText") as TextBlock;
                tb.Text = value;
            }
        }

        protected override void Animate()
        {
            if (IsLoaded)
            {
                SetColourFromRange();
                int animateTo = Value + 11;

                Storyboard sb = Root.FindName("_moveMerc") as Storyboard;
                SplineDoubleKeyFrame endFrame = sb.FindName("_mercPos") as SplineDoubleKeyFrame;
                endFrame.Value = -animateTo;
                sb.Begin();
            }
        }


    }
}
