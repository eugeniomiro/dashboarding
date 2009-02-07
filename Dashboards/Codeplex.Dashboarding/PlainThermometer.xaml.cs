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
        /// Initialize the animation end-points
        /// </summary>
        void InitializeAnimation()
        {
            _fullScale = _scale.Value;
            _fullTranslate = _translate.Value;
        }


        #region MercuryColorRange property

        /// <summary>
        /// Dependancy property for out MercuryColor property
        /// </summary>
        public static readonly DependencyProperty MercuryColorRangeProperty =
            DependencyProperty.Register("MercuryColorRange", typeof(ColorPointCollection), typeof(PlainThermometer), new PropertyMetadata(new PropertyChangedCallback(MercuryColorRangeChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
        /// </summary>
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
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MercuryColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PlainThermometer instance = dependancy as PlainThermometer;
            if (instance != null)
            {
                instance.Animate();
            }
        }


        #endregion

        #region TextColor property

        /// <summary>
        /// The dependancy property for the TextColor property
        /// </summary>
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(PlainThermometer), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Color of the text that shows the percentage
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PlainThermometer instance = dependancy as PlainThermometer;


            if (instance != null)
            {
                instance._text.Foreground = new SolidColorBrush(instance.TextColor);
            }
        }

        #endregion

        #region TextVisibility property
        /// <summary>
        /// The dependancy property for theTextVisibilityiColor property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(PlainThermometer), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        /// <summary>
        /// Visibility of the text that shows the percentage
        /// </summary>
        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set
            {
                SetValue(TextVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PlainThermometer instance = dependancy as PlainThermometer;


            if (instance != null)
            {
                instance._text.Visibility = instance.TextVisibility;
            }
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
            MoveCurrentPositionByOffset(mouseDownPosition.Y - currentPosition.Y);
            Animate();
        }


        #endregion


        #region privates
        /// <summary>
        /// Sets the high and low colors from the Mercury color range
        /// </summary>
        private void SetMercuryColor()
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

            

            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {
                

                SetMercuryColor();
                _translate.Value = _fullTranslate * NormalizedValue;
                _scale.Value = _fullScale * NormalizedValue;
                _text.Text = "" + Value;
                _grabFrame.Value = -(NormalizedValue * 100);
                _swipe.Begin();
            }
            else
            {
              
                TransformGroup handle  = _grabHandleCanvas.RenderTransform as TransformGroup;
                handle.Children[3].SetValue(TranslateTransform.YProperty, -(CurrentNormalizedValue*100));

                TransformGroup merc = _merc.RenderTransform as TransformGroup;
                merc.Children[0].SetValue(ScaleTransform.ScaleYProperty, _fullScale * (CurrentNormalizedValue ));
                merc.Children[3].SetValue(TranslateTransform.YProperty, _fullTranslate * (CurrentNormalizedValue ));
                _text.Text = "" + CurrentValue;
            }

          


           
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
