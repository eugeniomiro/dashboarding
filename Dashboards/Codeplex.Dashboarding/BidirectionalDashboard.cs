
#region Copyright 2008 David Black

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
    /// A bidirectionalDashboard can both display and set values. Increasingly
    /// analysts claim that showing data is not enough and that interaction is key.
    /// 
    /// </summary>
    public abstract class BidirectionalDashboard : Dashboard
    {

        #region private members

        /// <summary>
        /// Our grab handle 
        /// </summary>
        internal FrameworkElement GrabHandle { get; set; }

        #endregion

        #region protected members


        /// <summary>
        /// THe handle is grabbed, child controls should not render other than on
        /// mouse move etc
        /// </summary>
        protected bool IsGrabbed { get; set; }

        /// <summary>
        /// Current control Normalized value while dragging
        /// </summary>
        protected double CurrentNormalizedValue { get; set; }

        /// <summary>
        /// The current value of the drap point in the range Minimum <= CurrentValue <= Maximum
        /// </summary>
        public double CurrentValue 
        {
            get
            {
                return Minimum + ((Maximum - Minimum) * CurrentNormalizedValue);
            }
        }

        /// <summary>
        /// Manipulates the CurrentNormalizedValue to move the position by a number of screen pixels
        /// </summary>
        /// <param name="offset">Offset in pixels</param>
        protected void MoveCurrentPositionByOffset(double offset)
        {
            CurrentNormalizedValue = NormalizedValue + (offset / 100);
            if (CurrentNormalizedValue > 1)
            {
                CurrentNormalizedValue = 1;
            }
            if (CurrentNormalizedValue < 0)
            {
                CurrentNormalizedValue = 0;
            }
        }

        #endregion


        /// <summary>
        /// Constrcts a BidirectionalDashboard, which is mostly about grabbing the mouse
        /// enter and leave events and rendering the focus handle is necessary
        /// </summary>
        public BidirectionalDashboard()
        {
            IsGrabbed = false;
        }

        /// <summary>
        /// Register the control that the grab action works upon
        /// </summary>
        /// <param name="target"></param>
        protected void RegisterGrabHandle(FrameworkElement target)
        {
            GrabHandle = target;
            if (IsBidirectional)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            if (GrabHandle != null)
            {
                GrabHandle.Cursor = (IsBidirectional) ? Cursors.Hand: Cursors.None;
                GrabHandle.MouseEnter += new MouseEventHandler(BidirectionalDashboard_MouseEnter);
                GrabHandle.MouseLeave += new MouseEventHandler(GrabHandle_MouseLeave);
                GrabHandle.MouseLeftButtonUp += new MouseButtonEventHandler(BidirectionalDashboard_MouseLeftButtonUp);
                GrabHandle.MouseLeftButtonDown += new MouseButtonEventHandler(target_MouseLeftButtonDown);
                GrabHandle.MouseMove += new MouseEventHandler(GrabHandle_MouseMove);
            }
        }

        




        #region IsBidirectional property

        /// <summary>
        /// Identifies the IsBidirectional attached property
        /// </summary>
        public static readonly DependencyProperty IsBidirectionalProperty =
            DependencyProperty.Register("IsBidirectional", typeof(Boolean), typeof(BidirectionalDashboard), new PropertyMetadata(new PropertyChangedCallback(IsBidirectionalPropertyChanged)));

        /// <summary>
        /// Gets or sets a value to determin if this dashboard is bidrectional. IsBiderectional == false means that
        /// the control shows values. IsBidirectional == true means that the user can interact with the control
        /// to ser values.
        /// </summary>
        public Boolean IsBidirectional
        {
            get
            {
                Boolean res = (Boolean)GetValue(IsBidirectionalProperty);
                return res;
            }
            set
            {
                SetValue(IsBidirectionalProperty, value);
                Initialize();
            }
        }



        /// <summary>
        /// The value of the IsBidirectional property has changed. We call animate to allow any focus
        /// handle to be rendered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void IsBidirectionalPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDashboard instance = dependancy as BidirectionalDashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("IsBidirectional");
                instance.Animate();
            }
        }


        #endregion

        #region mouse enter and leave

        /// <summary>
        /// Mouse has entered the control if we are showing focus and we are bidirectional
        /// we call animate to get the child control to render
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        internal void BidirectionalDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsBidirectional )
            {
                

                ShowGrabHandle();
            }
        }


       
        private Point _grabOrigin;

        /// <summary>
        /// The mouse has been clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        void target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ButtonDown(e.GetPosition(this));
        }

        /// <summary>
        /// Left button down exposed here for unit testing
        /// </summary>
        /// <param name="at"></param>
         internal void ButtonDown(Point at)
         {
             IsGrabbed = true;
             CurrentNormalizedValue = Value;
             if (GrabHandle != null)
             {
                 GrabHandle.CaptureMouse();
             }
             _grabOrigin = at;
             // user may click-release-click on the grab handle, no mouse in event occurs so we show focus here too
             ShowGrabHandle();
         }

        /// <summary>
        /// Mouse has moved pass on the origin and current position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabHandle_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);

            MoveToPoint(p);
        }

        /// <summary>
        /// The mouse has moved to a new point. Deal with it (exposed for unit testing ony)
        /// </summary>
        /// <param name="p">THe mouse point</param>
        internal void MoveToPoint(Point p)
        {
            if (IsBidirectional && IsGrabbed)
            {
                OnMouseGrabHandleMove(_grabOrigin, p);
            }
        }
        


        /// <summary>
        /// Button goes up if we are grabbing we do te OnSetValueCall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         void BidirectionalDashboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseUp();
        }

        /// <summary>
        /// Mouse is up internal for unit-tests
        /// </summary>
         internal void MouseUp()
         {
             if (IsGrabbed)
             {
                 Value = CurrentValue;
                 HideGrabHandle();
                 if (GrabHandle != null)
                 {
                     GrabHandle.ReleaseMouseCapture();
                 }
                 Animate();
             }
             else
             {
                 Animate();
             }
             IsGrabbed = false;
         }

        /// <summary>
        /// THe mouse has left the control if there is no grab then remove the focus handle
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event args</param>
        internal void GrabHandle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsGrabbed && IsBidirectional)
            {
                HideGrabHandle();
            }
        }

        /// <summary>
        /// The mouse has entered the control registered as the grab handle, show the focus control
        /// </summary>
        protected virtual void ShowGrabHandle()
        {
        }

        /// <summary>
        /// The mouse has exited the control registered as the grab handle, hide the focus control
        /// </summary>
        protected virtual void HideGrabHandle()
        {
        }

       
        /// <summary>
        /// We have a mouse down and move event, we pass the point where the original click happened
        /// and the current point
        /// </summary>
        /// <param name="mouseDownPosition">Point this all happend at</param>
        /// <param name="currentPosition">Where we are now</param>
        protected virtual void OnMouseGrabHandleMove(Point mouseDownPosition, Point currentPosition)
        {
           
        }

        #endregion

    }



}
