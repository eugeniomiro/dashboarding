using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Dials
{
    public class White360 : WebServiceEnabledDashboard
    {

        public White360(): base("CodePlex.Dashboarding.Silverlight.Dials.White360.xaml")
        {
        }

        protected override void Animate()
        {
            if (IsLoaded)
            {
                SetColourFromRange();
                int animateTo = -150 + (3 * Value);
                Storyboard sb = Root.FindName("_moveNeedle") as Storyboard;
                SplineDoubleKeyFrame endFrame = sb.FindName("_needlePos") as SplineDoubleKeyFrame;
                endFrame.Value = animateTo;
                sb.Begin();
            }
        }
    }
}
