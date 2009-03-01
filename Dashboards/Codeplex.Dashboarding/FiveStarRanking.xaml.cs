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
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A five star ranking is a sliding scale of stars, hearts etc. The character dispayed is
    /// set by the Character property.
    /// <para>The colors specifed are the InRank and OutRank colors. The InRank colors are the highlights</para>
    /// </summary>
    public partial class FiveStarRanking : BidirectionalDashboard
    {
        /// <summary>
        /// Constructs a FiveStarRanking
        /// </summary>
        public FiveStarRanking() 
        {
            InitializeComponent();
            RegisterGrabHandle(LayoutRoot);
            RegisterGrabHandle(_grabHandleCanvas);
        }

        #region InRankColor property

        /// <summary>
        /// Dependancy Property for the InRankColor property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty InRankColorProperty =
            DependencyProperty.Register("InRankColor", typeof(ColorPoint), typeof(FiveStarRanking), new PropertyMetadata(new PropertyChangedCallback(InRankColorPropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public ColorPoint InRankColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(InRankColorProperty);
                return res;
            }
            set
            {
                SetValue(InRankColorProperty, value);
                this.Animate();
            }
        }



        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void InRankColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            FiveStarRanking instance = dependancy as FiveStarRanking;
            if (instance != null)
            {
                instance.SetInRankColor();
            }
        }

        private void SetInRankColor()
        {
            _highEnabled0.Color = InRankColor.HiColor;
            _highEnabled1.Color = InRankColor.HiColor;
            _highEnabled2.Color = InRankColor.HiColor;
            _highEnabled3.Color = InRankColor.HiColor;
            _highEnabled4.Color = InRankColor.HiColor;

            _lowEnabled0.Color = InRankColor.LowColor;
            _lowEnabled1.Color = InRankColor.LowColor;
            _lowEnabled2.Color = InRankColor.LowColor;
            _lowEnabled3.Color = InRankColor.LowColor;
            _lowEnabled4.Color = InRankColor.LowColor;
        }


        #endregion

        #region OutRankColor property

        /// <summary>
        /// Dependancy Property for the OutRankColor
        /// </summary>


        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "OutRank")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty OutRankColorProperty =
            DependencyProperty.Register("OutRankColor", typeof(ColorPoint), typeof(FiveStarRanking), new PropertyMetadata(new PropertyChangedCallback(OutRankColorPropertyChanged)));

        /// <summary>
        /// Sets the color range for when the value is false. Please see the definition of
        /// TrueColor range for a vacuous example of when ths may be needed
        /// </summary>

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "OutRank")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public ColorPoint OutRankColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(OutRankColorProperty);
                return res;
            }
            set
            {
                SetValue(OutRankColorProperty, value);
                this.Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void OutRankColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            FiveStarRanking instance = dependancy as FiveStarRanking;
            if (instance != null)
            {
                instance.SetOutRankColor();
            }
        }

        private void SetOutRankColor()
        {
            _highDisabled0.Color = OutRankColor.HiColor;
            _highDisabled1.Color = OutRankColor.HiColor;
            _highDisabled2.Color = OutRankColor.HiColor;
            _highDisabled3.Color = OutRankColor.HiColor;
            _highDisabled4.Color = OutRankColor.HiColor;
            _lowDisabled0.Color = OutRankColor.LowColor;
            _lowDisabled1.Color = OutRankColor.LowColor;
            _lowDisabled2.Color = OutRankColor.LowColor;
            _lowDisabled3.Color = OutRankColor.LowColor;
            _lowDisabled4.Color = OutRankColor.LowColor;
        }

        #endregion


        #region BiDirection
        /// <summary>
        /// Highlight the grab handle as the mouse is in
        /// </summary>
        protected override void ShowGrabHandle()
        {
            base.ShowGrabHandle();
            _grabHandle.StrokeDashArray = new DoubleCollection { 1, 1 };
            _grabHandleCanvas.Background = new SolidColorBrush(Color.FromArgb(0x4c, 0xde, 0xf0, 0xf6));
        }

        /// <summary>
        /// Stop the highlight of the grab handle the mouse is out
        /// </summary>
        protected override void HideGrabHandle()
        {
            base.HideGrabHandle();
            _grabHandle.StrokeDashArray = new DoubleCollection();
            _grabHandleCanvas.Background = new SolidColorBrush(Colors.Transparent);
        }


        /// <summary>
        /// Mouse is moving, move the diagram
        /// </summary>
        /// <param name="mouseDownPosition">origin of the drag</param>
        /// <param name="currentPosition">where the mouse is now</param>
        protected override void OnMouseGrabHandleMove(Point mouseDownPosition, Point currentPosition)
        {
            base.OnMouseGrabHandleMove(mouseDownPosition, currentPosition);
            MoveCurrentPositionByOffset(currentPosition.X - mouseDownPosition.X);
            this.Animate();
        }


        #endregion



        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {

            if (IsBidirectional)
            {
                _grabHandleCanvas.Visibility = Visibility.Visible;
                _grabHandle.Visibility = Visibility.Visible;
            }
            else
            {
                _grabHandleCanvas.Visibility = Visibility.Collapsed;
                _grabHandle.Visibility = Visibility.Collapsed;
            }


            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {
                SetPointerByAnimationOverSetTime(NormalizedValue, AnimationDuration);
            }
            else
            {
                SetPointerByAnimationOverSetTime(CurrentNormalizedValue, TimeSpan.Zero);
            }
        
        }


        /// <summary>
        /// Sets the pointer animation to execute and sets the time to animate. This allow the same
        /// code to handle normal operation using the Dashboard.AnimationDuration or for dragging the
        /// needle during bidirectional operation (TimeSpan.Zero)
        /// </summary>
        /// <param name="normalizedValue">The normalized Value.</param>
        /// <param name="duration">The duration.</param>
        private void SetPointerByAnimationOverSetTime(double normalizedValue, TimeSpan duration)
        {
            double pos = normalizedValue * 100;
            PointAnimation pa = GetChildPointAnimation(AnimateIndicatorStoryboard, "_animOrigin");
            pa.To = new Point(pos, 0);
            pa.Duration = new Duration(duration);
            pa = GetChildPointAnimation(AnimateIndicatorStoryboard, "_animTopLeft");
            pa.To = new Point(pos, 0);
            pa.Duration = new Duration(duration);
            pa = GetChildPointAnimation(AnimateIndicatorStoryboard, "_animBotRight");
            pa.To = new Point(pos, 32);
            pa.Duration = new Duration(duration);

            Start(AnimateIndicatorStoryboard);
            SplineDoubleKeyFrame s = SetFirstChildSplineDoubleKeyFrameTime(AnimateGrabHandleStoryboard, pos);
            s.KeyTime = KeyTime.FromTimeSpan(duration);
            Start(AnimateGrabHandleStoryboard);

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
    }
}
