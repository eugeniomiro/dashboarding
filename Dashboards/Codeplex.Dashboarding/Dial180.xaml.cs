//-----------------------------------------------------------------------
// <copyright file="Dial180.xaml.cs" company="David Black">
//      Copyright 2008 David Black
//  
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at
//     
//          http://www.apache.org/licenses/LICENSE-2.0
//    
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace Codeplex.Dashboarding
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// A needle and dial face control where the needle sweeps a 180 degree path. A Dial180 can be instantiated
    /// in XAML or in code. The XAML to create a simple Dial180 looks like
    /// 
    /// <pre lang="cs" numberLines="true" outlining="true" title="Example of Syntax Highlighting">
    ///     <db:Dial180>
    ///        <db:Dial180.FaceColorRange>
    ///           <db:ColorPoint HiColor="#2A242C" LowColor="#6C0680" Value="0" />
    ///           <db:ColorPoint HiColor="#2A242C" LowColor="#220000" Value="55" />
    ///       </db:Dial180.FaceColorRange>
    ///       <db:Dial180.NeedleColorRange>
    ///           <db:ColorPoint HiColor="#009900" LowColor="#44DD00" Value="0" />
    ///           <db:ColorPoint HiColor="#9DC800" LowColor="#DDCC00" Value="33" />
    ///           <db:ColorPoint HiColor="#660000" LowColor="#BB3300" Value="66" />
    ///        </db:Dial180.NeedleColorRange>
    ///    </db:Dial180>
    /// </pre>
    /// 
    /// </summary>
    public partial class Dial180 : BidirectionalDial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dial180"/> class.
        /// </summary>
        public Dial180()
        {
            InitializeComponent();
            RegisterGrabHandle(_grabHandle);
        }

        /// <summary>
        /// Gets the shape used to highlight the grab control
        /// </summary>
        protected override Shape GrabHighlight
        {
            get { return _grabHighlight; }
        }

        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected override Grid ResourceRoot
        {
            get { return LayoutRoot; }
        }

        #region protected methods

        /// <summary>
        /// Requires that the control hounours all appearance setting as specified in the
        /// dependancy properties (at least the supported ones). No dependancy property handling
        /// is performed until all dependancy properties are set and the control is loaded.
        /// </summary>
        protected override void ManifestChanges()
        {
            this.UpdateFaceColor();
            this.UpdateNeedleColor();
            this.UpdateTextColor();
            this.UpdateTextFormat();
            this.UpdateTextVisibility();
        }

        /// <summary>
        /// Sets the face color from the color range
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected override void UpdateFaceColor()
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected override void UpdateNeedleColor()
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
        protected override void UpdateTextColor()
        {
            for (int i = 0; i <= 4; i++)
            {
                TextBlock tb = LayoutRoot.FindName("_txt" + i) as TextBlock;
                if (tb != null)
                {
                    tb.Foreground = new SolidColorBrush(FaceTextColor);
                }
            }

            _text.Foreground = new SolidColorBrush(ValueTextColor);
        }

        /// <summary>
        /// Sets the text visibility to that of the TextVisibility property
        /// </summary>
        protected override void UpdateTextVisibility()
        {
            for (int i = 0; i <= 4; i++)
            {
                TextBlock tb = LayoutRoot.FindName("_txt" + i) as TextBlock;
                if (tb != null)
                {
                    tb.Visibility = FaceTextVisibility;
                }
            }

            _text.Visibility = ValueTextVisibility;
        }

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected override void UpdateTextFormat()
        {
            for (int i = 0; i <= 4; i++)
            {
                TextBlock tb = LayoutRoot.FindName("_txt" + i) as TextBlock;
                if (tb != null && FaceTextFormat != null)
                {
                    tb.Text = String.Format(FaceTextFormat, RealMinimum + (i * ((RealMaximum - RealMinimum) / 4)));
                }
            }

            if (_text != null)
            {
                _text.Text = this.IsGrabbed ? FormattedCurrentValue : FormattedValue;
            }
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
        /// <param name="currentPoint">Mouse position</param>
        /// <returns>The angle in degrees</returns>
        protected override double CalculateRotationAngle(Point currentPoint)
        {
            double opposite = currentPoint.Y - (172 / 2);
            double adjacent = currentPoint.X - (ActualWidth / 2);
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (currentPoint.X >= (ActualWidth / 2) && currentPoint.Y <= (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else if (currentPoint.X < (ActualWidth / 2) && currentPoint.Y <= (172 / 2))
            {
                // already done
            }
            else if (currentPoint.X >= (ActualWidth / 2) && currentPoint.Y > (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }

            return angleInDegrees;
        }

        /// <summary>
        /// Move the needle and set the needle and face colors to suite the value
        /// </summary>
        protected override void Animate()
        {
            this.UpdateFaceColor();
            this.UpdateNeedleColor();

            this.ShowHandleIfBidirectional();

            if (!this.IsBidirectional || (this.IsBidirectional && !this.IsGrabbed))
            {
                this.SetPointerByAnimationOverSetTime(NormalizedValue, Value, AnimationDuration);
            }
            else
            {
                this.SetPointerByAnimationOverSetTime(CurrentNormalizedValue, CurrentValue, TimeSpan.Zero);
            }
        }
        #endregion

        #region private methods

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
            this.UpdateTextFormat();

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
        private void ShowHandleIfBidirectional()
        {
            Visibility val = IsBidirectional ? Visibility.Visible : Visibility.Collapsed;
            _grabHandle.Visibility = val;
            _grabHighlight.Visibility = val;
        }

        #endregion
    }
}
