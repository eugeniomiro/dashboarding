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
    /// A progress bar show the 0 .. 100% progress of an operation
    /// </summary>
    public partial class ProgressBar : BidirectionalDashboard
    {
        /// <summary>
        /// Constructs a ProgressBar
        /// </summary>
        public ProgressBar()
        {
            InitializeComponent();
            RegisterGrabHandle(_grabHandleCanvas);
        }

        #region OutlineColor property
        /// <summary>
        /// The dependancy color for the OutlineColor property
        /// </summary>
        public static readonly DependencyProperty OutlineColorProperty =
            DependencyProperty.Register("OutlineColor", typeof(Color), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public Color OutlineColor
        {
            get { return (Color)GetValue(OutlineColorProperty); }
            set
            {
                SetValue(OutlineColorProperty, value);
            }
        }

        /// <summary>
        /// One of our dependany properties have changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;


            if (instance != null)
            {
                if (instance.OutlineColor != null)
                {
                    instance._grid.Stroke = new SolidColorBrush(instance.OutlineColor);
                    instance._coloured.Stroke = new SolidColorBrush(instance.OutlineColor);
                    instance._gray.Stroke = new SolidColorBrush(instance.OutlineColor);
                }
            }
        }

        #endregion

        #region InProgressColor property

        /// <summary>
        /// Dependancy Property for the InProgressColor property
        /// </summary>
        public static readonly DependencyProperty InProgressColorProperty =
            DependencyProperty.Register("InProgressColor", typeof(ColorPoint), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(InProgressColorPropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        public ColorPoint InProgressColor
        {
            get
            {
                
                ColorPoint res = (ColorPoint)GetValue(InProgressColorProperty);
                return res;
            }
            set
            {
                SetValue(InProgressColorProperty, value);
                Animate();
            }
        }



        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void InProgressColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;
            if (instance != null)
            {
                instance.SetInProgressColor();
            }
        }

        private void SetInProgressColor()
        {
            _highEnabled0.Color = InProgressColor.HiColor;
            
            _lowEnabled0.Color = InProgressColor.LowColor;
        }


        #endregion

        #region OutOfProgressColor property

        /// <summary>
        /// Dependancy Property for the OutOfProgressColor
        /// </summary>
        public static readonly DependencyProperty OutOfProgressColorProperty =
            DependencyProperty.Register("OutOfProgressColor", typeof(ColorPoint), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(OutOfProgressColorPropertyChanged)));

        /// <summary>
        /// Sets the color range for when the value is false. Please see the definition of
        /// TrueColor range for a vacuous example of when ths may be needed
        /// </summary>
        public ColorPoint OutOfProgressColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(OutOfProgressColorProperty);
                return res;
            }
            set
            {
                SetValue(OutOfProgressColorProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void OutOfProgressColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;
            if (instance != null)
            {
                instance.SetOutOfProgressColor();
            }
        }

        private void SetOutOfProgressColor()
        {
            _highDisabled0.Color = OutOfProgressColor.HiColor;
           

            _lowDisabled0.Color = OutOfProgressColor.LowColor;
           

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
            Animate();
        }


        #endregion


        /// <summary>
        /// Calculate the destination position of the animation for the current value
        /// and set it off
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
                double pos = NormalizedValue * 100;
                _startPoint.To = new Point(pos, 0);
                _topLeft.To = new Point(pos, 0);
                _botLeft.To = new Point(pos, 15);
                _handlePos.Value = pos - 10;
                _swipe.Begin();
                _moveGrab.Begin();
            }
            else
            {
                double currentPos = CurrentNormalizedValue * 100;
                TransformGroup tg = _grabHandleCanvas.RenderTransform as TransformGroup;
                tg.Children[3].SetValue(TranslateTransform.XProperty, currentPos - 10);
                pf.StartPoint = new Point(currentPos, 0);
                seg1.Point = new Point(currentPos, 0);
                seg4.Point = new Point(currentPos, 15); 
            }
        }
    }
}
