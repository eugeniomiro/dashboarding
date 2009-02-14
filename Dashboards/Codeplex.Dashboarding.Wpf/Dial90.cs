﻿#region Copyright 2008 David Black

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

#endregion

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// Base class for all quadrant based 90 degree dials
    /// </summary>
    public abstract class Dial90 : BidirectionalDial
    {

        /// <summary>
        /// Canvas to rotate to move the grab handle
        /// </summary>
        private Canvas _grabHandleCanvas;

        /// <summary>
        /// Shape used to highlight that the mouse is in the grab area
        /// </summary>
        private Shape _grabHighlightShape;

        /// <summary>
        /// The text block used to display the percentage
        /// </summary>
        private TextBlock _textBlock;

        /// <summary>
        /// Blend start for the face of the dial
        /// </summary>
        private GradientStop _faceHighColorGradientStop;

        /// <summary>
        /// Blend end for the face of the dial
        /// </summary>
        private GradientStop _faceLowColorGradientStop;

        /// <summary>
        /// Blend start for the face of the dial
        /// </summary>
        private GradientStop _needleHighColorGradientStop;

        /// <summary>
        /// Blend end for the face of the dial
        /// </summary>
        private GradientStop _needleLowColorGradientStop;

        /// <summary>
        /// Displays the needle
        /// </summary>
        private Path _needlePath;

      

        /// <summary>
        /// Set the defaults for our dependancy properties and register the
        /// grab handle
        /// </summary>
        protected void InitializeDial90()
        {
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());
            InitialiseRefs();
            RegisterGrabHandle(_grabHandleCanvas);
        }

        /// <summary>
        /// Initialize references to controls we expect to find in the child
        /// </summary>
        private void InitialiseRefs()
        {
            _grabHandleCanvas = ResourceRoot.FindName("_grabHandle") as Canvas;
            _grabHighlightShape = ResourceRoot.FindName("_grabHighlight") as Shape;
            _textBlock = ResourceRoot.FindName("_text") as TextBlock;
            _faceHighColorGradientStop = ResourceRoot.FindName("_colourRangeStart") as GradientStop;
            _faceLowColorGradientStop = ResourceRoot.FindName("_colourRangeEnd") as GradientStop;
            _needleHighColorGradientStop = ResourceRoot.FindName("_needleHighColour") as GradientStop;
            _needleLowColorGradientStop = ResourceRoot.FindName("_needleLowColour") as GradientStop;
            _needlePath = ResourceRoot.FindName("path") as Path;
            
           
        }

        /// <summary>
        /// Return the shape used to highlight the grab control
        /// </summary>
        protected override Shape GrabHighlight
        {
            get { return _grabHighlightShape; }
        }

        /// <summary>
        /// Sets the text visibility to that of the TextVisibility property
        /// </summary>
        protected override void SetTextVisibility()
        {
            _textBlock.Visibility = TextVisibility;
        }

        /// <summary>
        /// Set our text color to that of the TextColorProperty
        /// </summary>
        protected override void SetTextColor()
        {
            _textBlock.Foreground = new SolidColorBrush(TextColor);
        }

        /// <summary>
        /// Sets the face color from the color range
        /// </summary>
        protected override void SetFaceColor()
        {

            ColorPoint c = FaceColorRange.GetColor(Value);
            if (c != null)
            {
                _faceHighColorGradientStop.Color = c.HiColor;
                _faceLowColorGradientStop.Color = c.LowColor;
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
                _needleHighColorGradientStop.Color = c.HiColor;
                _needleLowColorGradientStop.Color = c.LowColor;
            }
        }



        /// <summary>
        /// Based on the rotation angle, set the normalized current value
        /// </summary>
        /// <param name="cv">rotation angle</param>
        protected override void SetCurrentNormalizedValue(double cv)
        {
            cv = (cv < 0) ? 0 : cv;
            cv = (cv > 90) ? 90 : cv;
            CurrentNormalizedValue = cv / 90;
        }


        /// <summary>
        /// Shows the grab handle if this control is bidirectional
        /// </summary>
        protected void ShowHandleIfBiDirectional()
        {
            Visibility val = IsBidirectional ? Visibility.Visible : Visibility.Collapsed;

            _grabHandleCanvas.Visibility = val;
            _grabHighlightShape.Visibility = val;
        }

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

                _textBlock.Text = "" + Value;
                double point = CalculatePointFromNormalisedValue();

                SetFirstChildSplineDoubleKeyFrameTime(AnimateIndicatorStoryboard, point);
                AnimateIndicatorStoryboard.Begin();

                SetFirstChildSplineDoubleKeyFrameTime(AnimateGrabHandleStoryboard, point);
                AnimateGrabHandleStoryboard.Begin();

            }
            else
            {
                _textBlock.Text = String.Format("{0:000}", CurrentValue);

                double currentPos = CalculatePointFromCurrentNormalisedValue();

                TransformGroup tg = _needlePath.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, currentPos);

                tg = _grabHandleCanvas.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, currentPos);
            }
        }


        /// <summary>
        /// Calculate the rotation angle from the normalised actual value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected abstract double CalculatePointFromNormalisedValue();

        /// <summary>
        /// Calculate the rotation angle from the normalised current value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected abstract double CalculatePointFromCurrentNormalisedValue();


    }
}