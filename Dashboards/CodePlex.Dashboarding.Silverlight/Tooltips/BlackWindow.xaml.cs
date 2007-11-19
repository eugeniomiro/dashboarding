using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Tooltips
{
    public class BlackWindow : Tooltip
    {
        public BlackWindow(Canvas parent, string text) : base(parent, text, "CodePlex.Dashboarding.Silverlight.Tooltips.BlackWindow.xaml")
        {

        }

        public override void Animate()
        {
            if (IsLoaded)
            {
                Storyboard sb = Root.FindName("Timeline1") as Storyboard;
                if (sb != null)
                {
                    sb.Begin();
                }
            }
        }
    }
}
