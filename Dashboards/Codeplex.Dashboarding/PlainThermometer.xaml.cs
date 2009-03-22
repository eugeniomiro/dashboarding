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
using System.Diagnostics.CodeAnalysis;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A simple Thermometer, hold the lettuce
    /// </summary>
    public partial class PlainThermometer : BidirectionalDashboard
    {
        /// <summary>
        /// Stores the 100% end point values for the animation scale
        /// we multiple by (perc/100) to get mid points
        /// </summary>
        double _fullScale;

        /// <summary>
        /// Stores the 100% end point values for the animation tarnsform
        /// we multiple by (perc/100) to get mid points
        /// </summary>
        double _fullTranslate;



        /// <summary>
        /// Constructs a plai thermometer
        /// </summary>
        public PlainThermometer() : base()
        {
            InitializeComponent();
            SetValue(MercuryColorRangeProperty, new ColorPointCollection());
            RegisterGrabHandle(_grabHandleCanvas);
           
        }

        /// <summary>
        /// Requires that the control hounours all appearance setting as specified in the
        /// dependancy properties (at least the supported ones). No dependancy property handling
        /// is performed until all dependancy properties are set and the control is loaded.
        /// </summary>
        protected override void ManifestChanges()
        {
            UpdateMercuryColor();
            UpdateTextColor();
            UpdateTextFormat();
            UpdateTextVisibility();
        }

        /// <summary>
        /// Initialize the animation end-points
        /// </summary>
        void InitializeAnimation()
        {
            DoubleAnimationUsingKeyFrames da = GetChildDoubleAnimationUsingKeyFrames(AnimateIndicatorStoryboard, "_scaleContainer");
            _fullScale =  da.KeyFrames[0].Value;
            da = GetChildDoubleAnimationUsingKeyFrames(AnimateIndicatorStoryboard, "_translateContainer");
            _fullTranslate = da.KeyFrames[0].Value;
        }


        #region MercuryColorRange property

        /// <summary>
        /// Dependancy property for out MercuryColor property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty MercuryColorRangeProperty =
            DependencyProperty.Register("MercuryColorRange", typeof(ColorPointCollection), typeof(PlainThermometer), new PropertyMetadata(new PropertyChangedCallback(MercuryColorRangeChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is bound to xaml and the colection does change!")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public ColorPointCollection MercuryColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(MercuryColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(MercuryColorRangeProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        private static void MercuryColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PlainThermometer instance = dependancy as PlainThermometer;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.Animate();
            }
        }


        #endregion


        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        protected override void UpdateTextColor()
        {
            _text.Foreground = new SolidColorBrush(ValueTextColor);
        }

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected override void UpdateTextVisibility()
        {
            _text.Visibility = ValueTextVisibility;
        }

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected override void UpdateTextFormat()
        {
            if (this._text != null)
            {
                this._text.Text = IsGrabbed ? FormattedCurrentValue : FormattedValue;
            }
        }

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
            MoveCurrentPositionByOffset(mouseDownPosition.Y - currentPosition.Y);
            Animate();
        }


        #endregion


        #region privates
        /// <summary>
        /// Sets the high and low colors from the Mercury color range
        /// </summary>
        private void UpdateMercuryColor()
        {
            ColorPoint c = MercuryColorRange.GetColor(Value);
            if (c != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    GradientStop gs = LayoutRoot.FindName("_mercL" + i) as GradientStop;
                    if (gs != null)
                    {
                        gs.Color = c.LowColor;
                    }
                    gs = LayoutRoot.FindName("_mercH" + i) as GradientStop;
                    if (gs != null)
                    {
                        gs.Color = c.HiColor;
                    }
                }
            }
        }


        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {
            UpdateTextFormat();

            if (_fullScale == 0 || _fullTranslate == 0)
            {
                InitializeAnimation();
            }

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

            UpdateMercuryColor();

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
        /// <param name="nv">The normalized Value.</param>
        /// <param name="duration">The duration.</param>
        private void SetPointerByAnimationOverSetTime(double nv, TimeSpan duration)
        {
            UpdateMercuryColor();

            DoubleAnimationUsingKeyFrames da = GetChildDoubleAnimationUsingKeyFrames(AnimateIndicatorStoryboard, "_scaleContainer");
            da.KeyFrames[0].Value = _fullScale * nv;
            da.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(duration);
            da = GetChildDoubleAnimationUsingKeyFrames(AnimateIndicatorStoryboard, "_translateContainer");
            da.KeyFrames[0].Value = _fullTranslate * nv;
            da.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(duration);
            da = GetChildDoubleAnimationUsingKeyFrames(AnimateIndicatorStoryboard, "_animGrab");
            da.KeyFrames[0].Value = -(nv * 100);
            da.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(duration);
            Start(AnimateIndicatorStoryboard);
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
