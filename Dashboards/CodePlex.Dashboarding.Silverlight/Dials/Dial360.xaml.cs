using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodePlex.Dashboarding.Silverlight.Common;

namespace CodePlex.Dashboarding.Silverlight.Dials
{
    public class Dial360 : WebServiceEnabledDashboard
    {
        private string _textColour;
        private string _colourRangeStart;
        private string _colourRangeEnd;


        public Dial360(): base("CodePlex.Dashboarding.Silverlight.Dials.Dial360.xaml")
        {
        }



        public string ColourRangeStart
        {
            get { return _colourRangeStart; }
            set 
            { 
                _colourRangeStart = value;
                GradientStop stop = Root.FindName("_colourRangeStart") as GradientStop;
                stop.Color = Bound.GetColour(_colourRangeStart);
            }
        }

        public string ColourRangeEnd
        {
            get { return _colourRangeEnd; }
            set 
            { 
                _colourRangeEnd = value;
                GradientStop stop = Root.FindName("_colourRangeEnd") as GradientStop;
                stop.Color = Bound.GetColour(_colourRangeEnd);
            }
        }

        public string TextColour
        {
            get { return _textColour; }
            set
            {
                _textColour = value;
                SetTextColour(MainCanvas.Children, Bound.GetColour(_textColour));
            }
        }

       


        protected override void Animate()
        {
            if (IsLoaded)
            {
                if (Value != null)
                {
                    PercentageText = String.Format("{0:000}", ValueAsInteger);
                    SetColourFromRange();
                    int animateTo = -150 + (3 * ValueAsInteger);
                    Storyboard sb = Root.FindName("_moveNeedle") as Storyboard;
                    SplineDoubleKeyFrame endFrame = sb.FindName("_needlePos") as SplineDoubleKeyFrame;
                    endFrame.Value = animateTo;
                    sb.Begin();
                }
            }
        }


        private void SetTextColour(VisualCollection visualCollection, Color color)
        {
            foreach (Visual v in visualCollection)
            {
                TextBlock tb = v as TextBlock;
                if (tb != null)
                {
                    tb.Foreground = new SolidColorBrush(color);
                }
            }
        }
    }
}
