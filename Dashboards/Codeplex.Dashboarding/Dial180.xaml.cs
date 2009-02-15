/* -------------------------------------------------------------------------
 *     
 *  Copyright 2008 David Black
 *  
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *     
 *     http://www.apache.org/licenses/LICENSE-2.0
 *    
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 *  -------------------------------------------------------------------------
 */

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
    /// A needle and dial face control where the needle sweeps a 180 degree path. 
    /// </summary>
    public partial class Dial180 : BidirectionalDial
    {
        /// <summary>
        /// constructs a dial 180 
        /// </summary>
        public Dial180()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());
            RegisterGrabHandle(_grabHandle);
        }

        #region abstract BiDirectionalDial implementation


        /// <summary>
        /// Sets the face color from the color range
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
        /// Sets the needle color from the color range
        /// </summary>
        protected override void SetNeedleColor()
        {
            ColorPoint c = NeedleColorRange.GetColor(Value);
            if (c != null)
            {
                _needleHighColour.Color = c.HiColor;
                _needleLowColour.Color = c.LowColor;
            }
        }

        /// <summary>
        /// Set our text color to that of the TextColorProperty
        /// </summary>
        protected override void SetTextColor()
        {
            _text.Foreground = new SolidColorBrush(TextColor);
        }

        /// <summary>
        /// Sets the text visibility to that of the TextVisibility property
        /// </summary>
        protected override void SetTextVisibility()
        {
            _text.Visibility = TextVisibility;
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
            cv = (cv < 0) ? 0 : cv;
            cv = (cv > 180) ? 180 : cv;
            CurrentNormalizedValue = cv / 180;
        }

        /// <summary>
        /// Determines the angle of the needle based on the mouse 
        /// position.
        /// </summary>
        /// <param name="_currentPoint">Mouse position</param>
        /// <returns>The angle in degrees</returns>
        protected override double CalculateRotationAngle(Point _currentPoint)
        {

            double opposite = _currentPoint.Y - (172 / 2);
            double adjacent = _currentPoint.X - (ActualWidth / 2);
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y <= (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else if (_currentPoint.X < (ActualWidth / 2) && _currentPoint.Y <= (172 / 2))
            {
                // already done
            }
            else if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y > (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else
            {
                //angleInDegrees = 360 + angleInDegrees;
            }

            return angleInDegrees;
        }



        #endregion

        #region Animation

        /// <summary>
        /// Move the needle and set the needle and face colors to suite the value
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();

            ShowHandleIfBiDirectional();

            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {
                SetPointerByAnimationOverSetTime(NormalizedValue, Value, AnimationDuration);
            }
            else
            {
                SetPointerByAnimationOverSetTime(CurrentNormalizedValue, CurrentValue, TimeSpan.Zero);
            
            
            }
        }

        /// <summary>
        /// Sets the pointer animation to execute and sets the time to animate. This allow the same
        /// code to handle normal operation using the Dashboard.AnimationDuration or for dragging the
        /// needle during bidirectional operation (TimeSpan.Zero)
        /// </summary>
        /// <param name="normalizedValue">The normalized Value.</param>
        /// <param name="value">The value.</param>
        /// <param name="duration">The duration.</param>
        private void SetPointerByAnimationOverSetTime(double normalizedValue, double value, TimeSpan duration)
        {
            _text.Text = "" + value;

            double point = -90 + (normalizedValue * 180);
            SplineDoubleKeyFrame needle = SetFirstChildSplineDoubleKeyFrameTime(AnimateIndicatorStoryboard, point);
            needle.KeyTime = KeyTime.FromTimeSpan(duration);
            Start(AnimateIndicatorStoryboard);

            SplineDoubleKeyFrame handle = SetFirstChildSplineDoubleKeyFrameTime(AnimateGrabHandleStoryboard, point);
            handle.KeyTime = KeyTime.FromTimeSpan(duration);
            Start(AnimateGrabHandleStoryboard);
        }

        /// <summary>
        /// Shows the grab handle if theis control is bidirectional
        /// </summary>
        private void ShowHandleIfBiDirectional()
        {
            Visibility val = IsBidirectional ? Visibility.Visible : Visibility.Collapsed;

            _grabHandle.Visibility = val;
            _grabHighlight.Visibility = val;
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
