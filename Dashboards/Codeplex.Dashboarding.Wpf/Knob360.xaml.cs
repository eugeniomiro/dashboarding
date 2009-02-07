using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// Dial 360 with a control knob to drag not a needle
    /// </summary>
    public partial class Knob360 : BidirectionalDial
    {
        /// <summary>
        /// Constructs a Knob360
        /// </summary>
        public Knob360()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());
            RegisterGrabHandle(_indicator);
        }

        #region Animate

        /// <summary>
        /// Animates the dial moving the grab knob and needle
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();

            _txt11.Text = String.Format("{0:000}", Value);

            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {               
                //_needlePos.Value = (-150+(300*NormalizedValue))-2;
                //_grabPos.Value =  (300*NormalizedValue) -10;
                SetFirstChildSplineDoubleKeyFrameTime(AnimateIndicatorStoryboard, (-150 + (300 * NormalizedValue)) - 2);
                AnimateIndicatorStoryboard.Begin();

                SetFirstChildSplineDoubleKeyFrameTime(AnimateGrabHandleStoryboard, (300 * NormalizedValue) - 10);
                AnimateGrabHandleStoryboard.Begin();

            }
            else
            {
                TransformGroup tg = _needle.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, (-150+(300*CurrentNormalizedValue))-2);

                tg = _grabCanvas.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, (300*CurrentNormalizedValue) -10);
            }
        }

        #endregion

        #region abstract BiDirectionalDial implementation

        /// <summary>
        /// Set the face color from the range
        /// </summary>
        protected override void SetFaceColor()
        {

            ColorPoint c = FaceColorRange.GetColor(Value);
            if (c != null)
            {
                _colourRangeStart.Color = c.HiColor;
                _colourRangeEnd.Color = c.LowColor;
            }
        }

        /// <summary>
        /// Set the needle color from the range
        /// </summary>
        protected override void SetNeedleColor()
        {
            ColorPoint c = NeedleColorRange.GetColor(Value);
            if (c != null)
            {
                _needle.Fill = new SolidColorBrush(c.HiColor);
            }
        }

        /// <summary>
        /// Set our text color to that of the TextColor property
        /// </summary>
        protected override void SetTextColor()
        {
            for (int i = 0; i <= 11; i++)
            {
                TextBlock tb = LayoutRoot.FindName("_txt" + i) as TextBlock;
                if (tb != null)
                {
                    tb.Foreground = new SolidColorBrush(TextColor);
                }
            }
        }

        /// <summary>
        /// Sets the text visibility to that of the TextVisibility property
        /// </summary>
        protected override void SetTextVisibility()
        {
            for (int i = 0; i <= 11; i++)
            {
                TextBlock tb = LayoutRoot.FindName("_txt" + i) as TextBlock;
                if (tb != null)
                {
                    tb.Visibility = TextVisibility;
                }
            }
        }

        /// <summary>
        /// Return the shape used to highlight the grab control
        /// </summary>
        protected override Shape GrabHighlight
        {
            get { return _grabHighlight; }
        }

        /// <summary>
        /// Based on the rotation angle, set the normalized current value
        /// </summary>
        /// <param name="cv">rotation angle</param>
        protected override void SetCurrentNormalizedValue(double cv)
        {
            if (cv < -150)
                cv = -150;
            if (cv > 150)
                cv = 150;
            CurrentNormalizedValue = (cv + 150) / 300;
        }

        /// <summary>
        /// Determines the angle of the needle based on the mouse 
        /// position.
        /// </summary>
        /// <param name="_currentPoint">Mouse position</param>
        /// <returns>The angle in degrees</returns>
        protected override double CalculateRotationAngle(Point _currentPoint)
        {
            double opposite = _currentPoint.Y - (ActualHeight / 2);
            double adjacent = _currentPoint.X - (ActualWidth / 2);
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y <= (ActualHeight / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else if (_currentPoint.X < (ActualWidth / 2) && _currentPoint.Y <= (ActualHeight / 2))
            {
                // already done
            }
            else if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y > (ActualHeight / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else
            {
                //no-op
            }
            angleInDegrees = (angleInDegrees - 90) % 360;
            return angleInDegrees;
        }


        #endregion

        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected override Grid ResourceRoot
        {
            get { return LayoutRoot; }
        }
    }
}
