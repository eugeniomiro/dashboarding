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
    /// A Dial360  displays as a traditional circular guage with numbers from 0 to 100. The
    /// needle sweep through aproximately 240 degrees
    /// </summary>
    public partial class Dial360 : BidirectionalDial
    {

        /// <summary>
        /// Constructs a Dial360
        /// </summary>
        public Dial360()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());
            RegisterGrabHandle(_grabHandle);
        }


        #region animation
        /// <summary>
        /// Animate our Dial360. We calculate the needle position, color and the face color
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();

            ShowIfBiDirectional();

            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {
                _txt11.Text = String.Format("{0:000}", Value);
                //SetColourFromRange();
                double animateTo = -150 + (3 * (NormalizedValue * 100));
                _needlePos.Value = animateTo;
                _grabPos.Value = animateTo;

                _moveGrab.Begin();
                _moveNeedle.Begin();
            }
            else
            {
                double currentPos = -150+ (3*(CurrentNormalizedValue * 100));
              
                TransformGroup tg = path.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, currentPos);

                tg = _grabHandle.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, currentPos);
              
            }


        }

       
        private void ShowIfBiDirectional()
        {
            Visibility val = IsBidirectional ? Visibility.Visible : Visibility.Collapsed;

            _grabHandle.Visibility = val;
            _grabHighlight.Visibility = val;
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
                _needleHighColour.Color = c.HiColor;
                _needleLowColour.Color = c.LowColor;
            }
        }

        /// <summary>
        /// Sets the text to the color in the TextColorProperty
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
        /// Converts the angle specified into a 0..1 normalized value
        /// </summary>
        /// <param name="cv"></param>
        protected override void SetCurrentNormalizedValue(double cv)
        {
            if (cv < -150)
                cv = -150;
            if (cv > 150)
                cv = 150;
            CurrentNormalizedValue = (cv + 150) / 300;
        }

        /// <summary>
        /// Based on the current position calculates what angle the current mouse
        /// position represents relative to the rotation point of the needle
        /// </summary>
        /// <param name="_currentPoint">Current point</param>
        /// <returns>Angle in degrees</returns>
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
                //angleInDegrees = 360 + angleInDegrees;
            }

            angleInDegrees = (angleInDegrees - 90) % 360;


            return angleInDegrees;
        }


        #endregion
    }
}
