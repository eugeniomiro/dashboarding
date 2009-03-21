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
    /// A diamond slider it a progress type gauge where a diamond shaped
    /// indicator is moved across a background rectangle.
    /// <para>The following can be customized
    /// <list type="bullet">
    /// <item>DiamondColor</item>
    /// <item>Progress bar gradient points (left, mid and right></item>
    /// </list>
    /// </para>
    /// </summary>
    public partial class DiamondSlider : BidirectionalDashboard
    {

        /// <summary>
        /// Constructs a DiamondSlider
        /// </summary>
        public DiamondSlider()
        {
            InitializeComponent();
            RegisterGrabHandle(_slider);
            DiamondColor = new Color { A = 0xFF, R = 0x85, G = 0x8a, B = 0xf9 };
            LeftGradient = new Color { A = 0xFF, R = 0xA0, G = 0xF0, B = 0x35 };
            RightGradient = new Color { A = 0xFF, R = 0xf9, G = 0x1d, B = 0x09 };
            MidGradient = new Color { A = 0xFF, R = 0xe4, G = 0xf7, B = 0x39 };
        }



        #region DiamondColorproperty
        /// <summary>
        /// The dependancy color for the DiamondColorproperty
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty DiamondColorProperty =
            DependencyProperty.Register("DiamondColor", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(DiamondColorPropertyChanged)));

        /// <summary>
        /// Color of the Diamond
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public Color DiamondColor
        {
            get { return (Color)GetValue(DiamondColorProperty); }
            set
            {
                SetValue(DiamondColorProperty, value);
            }
        }

        /// <summary>
        /// DiamondColor dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void DiamondColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateDiamondColor();
            }
        }

        private void UpdateDiamondColor()
        {
            if (_slider != null)
            {
                _slider.Fill = new SolidColorBrush(DiamondColor);
            }
        }

        #endregion

        #region LeftGradientproperty
        /// <summary>
        /// The dependancy color for the LeftGradient attached property
        /// </summary>
        public static readonly DependencyProperty LeftGradientProperty =
            DependencyProperty.Register("LeftGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(LeftGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the left hand side
        /// </summary>
        public Color LeftGradient
        {
            get { return (Color)GetValue(LeftGradientProperty); }
            set
            {
                SetValue(LeftGradientProperty, value);
            }
        }

        /// <summary>
        /// LeftGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void LeftGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null && instance.DashboardLoaded)
            {
                if (instance._slider != null)
                {
                    instance.UpdateLeftGradient();
                }
            }
        }

        private void UpdateLeftGradient()
        {
            if (LeftGradient != null)
            {
                _leftColor.Color = LeftGradient;
            }
        }
        #endregion

        #region MidGradientproperty
        /// <summary>
        /// The dependancy color for the MidGradientproperty
        /// </summary>
        public static readonly DependencyProperty MidGradientProperty =
            DependencyProperty.Register("MidGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(MidGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the mid point
        /// </summary>
        public Color MidGradient
        {
            get { return (Color)GetValue(MidGradientProperty); }
            set
            {
                SetValue(MidGradientProperty, value);
            }
        }

        /// <summary>
        /// MidGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MidGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance.UpdateMidGradient();
                }
            }
        }

        /// <summary>
        /// Updates the mid gradient color from the dependancy property.
        /// </summary>
        private void UpdateMidGradient()
        {
            if (MidGradient != null)
            {
                _midColor.Color = MidGradient;
            }
        }

        #endregion

        #region RightGradientproperty
        /// <summary>
        /// The dependancy color for the RightGradientproperty
        /// </summary>
        public static readonly DependencyProperty RightGradientProperty =
            DependencyProperty.Register("RightGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(RightGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the right hand side
        /// </summary>
        public Color RightGradient
        {
            get { return (Color)GetValue(RightGradientProperty); }
            set
            {
                SetValue(RightGradientProperty, value);
            }
        }

        /// <summary>
        /// RightGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void RightGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance.UpdateRightGradient();
                }
            }
        }

        /// <summary>
        /// Updates the right gradient color to that specified in the dependancy property.
        /// </summary>
        private void UpdateRightGradient()
        {
            if (RightGradient != null)
            {
                _rightColor.Color = RightGradient;
            }
        }
        #endregion

        /// <summary>
        /// Requires that the control hounours all appearance setting as specified in the
        /// dependancy properties (at least the supported ones). No dependancy property handling
        /// is performed until all dependancy properties are set and the control is loaded.
        /// </summary>
        protected override void ManifestChanges()
        {
            UpdateDiamondColor();
            UpdateLeftGradient();
            UpdateRightGradient();
            UpdateTextColor();
            UpdateTextFormat();
            UpdateTextVisibility();
        }

        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        protected override void UpdateTextColor()
        {
        }

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected override void UpdateTextVisibility()
        {
        }

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected override void UpdateTextFormat()
        {
        }

        /// <summary>
        /// Moves the diamond
        /// </summary>
        protected override void Animate()
        {
            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {
                SplineDoubleKeyFrame f = SetFirstChildSplineDoubleKeyFrameTime(AnimateIndicatorStoryboard, NormalizedValue * 100);
                f.KeyTime = KeyTime.FromTimeSpan(AnimationDuration);
                Start(AnimateIndicatorStoryboard);

            }
            else
            {
                SplineDoubleKeyFrame f = SetFirstChildSplineDoubleKeyFrameTime(AnimateIndicatorStoryboard, CurrentNormalizedValue * 100);
                f.KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
                Start(AnimateIndicatorStoryboard);
            }
        }

        /// <summary>
        /// Mouse has entered the grab handle, please give some visual feedback
        /// </summary>
        protected override void ShowGrabHandle()
        {
            base.ShowGrabHandle();
            _slider.StrokeThickness = 2;
            _slider.StrokeDashArray = new DoubleCollection { 1, 1 };
        }



        /// <summary>
        /// Remove the grab handle
        /// </summary>
        protected override void HideGrabHandle()
        {
            base.HideGrabHandle();
            _slider.StrokeThickness = 1;
            _slider.StrokeDashArray = new DoubleCollection();
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
