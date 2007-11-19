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
    public class TooltipFactory
    {
        public static Tooltip Create(Canvas parent, string text)
        {
            return new BlackWindow(parent, text);
        }
    }
}
