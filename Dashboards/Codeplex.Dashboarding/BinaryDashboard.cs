
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
    /// Common base class for for all binary dashboard controls. Contains
    /// properties to set the binary value and the colors for true and false
    /// representations of the control.
    /// </summary>
    public abstract class BinaryDashboard: Dashboard
    {
        /// <summary>
        /// Constructs a BinaryDashboard
        /// </summary>
        public BinaryDashboard() :base()
        {
            
            SetValue(TrueColorProperty, new ColorPoint { HiColor=Color.FromArgb(0xFF,0x6C,0xFA,0x20), LowColor = Color.FromArgb(0xFF,0xDC,0xF9,0xD4) });
            SetValue(FalseColorProperty, new ColorPoint { HiColor = Color.FromArgb(0xFF, 0xFA, 0x65, 0x65), LowColor = Color.FromArgb(0xFF, 0xFC, 0xD5, 0xD5) });
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PropertyHasChanged);
        }



        #region TrueColor property

        /// <summary>
        /// Identifies the TrueColor attached property
        /// </summary>
        public static readonly DependencyProperty TrueColorProperty =
            DependencyProperty.Register("TrueColor", typeof(ColorPoint), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(TrueColorPropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        public ColorPoint TrueColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(TrueColorProperty);
                return res;
            }
            set
            {
                SetValue(TrueColorProperty, value);
            }
        }


        /// <summary>
        /// Our TrueColor property has changed
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TrueColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("TrueColor");
                instance.Animate();
            }
        }


        #endregion

        #region FalseColor property

        /// <summary>
        /// Identifies our FalseColor attached property
        /// </summary>
        public static readonly DependencyProperty FalseColorProperty =
            DependencyProperty.Register("FalseColor", typeof(ColorPoint), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(FalseColorPropertyChanged)));

        /// <summary>
        /// Sets the color range for when the value is false. Please see the definition of
        /// TrueColor range for a vacuous example of when ths may be needed
        /// </summary>
        public ColorPoint FalseColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(FalseColorProperty);
                return res;
            }
            set
            {
                SetValue(FalseColorProperty, value);
            }
        }

        /// <summary>
        /// Our FalseColor dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void FalseColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("FalseColor");
                instance.Animate();
            }
        }

        #endregion

        #region IsTrue property

        /// <summary>
        /// Identifies the IsTrue attached property
        /// </summary>
        public static readonly DependencyProperty IsTrueProperty =
            DependencyProperty.Register("IsTrue", typeof(bool), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(IsTruePropertyChanged)));

        /// <summary>
        /// A boolean over ride of the value property from the Dashboard base class. Setting
        /// IsTrue = false sets Vaue to 0, setting IsTrue = true sets the value to 100
        /// </summary>
        public Boolean IsTrue
        {
            get
            {
                Boolean res = (Boolean)GetValue(IsTrueProperty);
                return res;
            }
            set
            {
                SetValue(IsTrueProperty, value);
            }
        }



        /// <summary>
        /// Our dependany property IsTrue has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void IsTruePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                double value = (instance.IsTrue) ? instance.Maximum : instance.Minimum;
                instance.SetValue(BinaryDashboard.ValueProperty, value);
                instance.OnPropertyChanged("IsTrue");
            }
        }


        #endregion

        #region private members
        /// <summary>
        /// Show or hised the correct element depending on the state and then starts
        /// any animation associated with the value
        /// </summary>
        /// <param name="trueControl">FrameWorkElement representing a true value</param>
        /// <param name="falseControl">FrameWorkElement representing false</param>
        /// <param name="sb">Storyboard to start</param>
        protected void PerformCommonBinaryAnimation(FrameworkElement trueControl, FrameworkElement falseControl, Storyboard sb)
        {
            if (trueControl != null || falseControl != null || sb != null)
            {
                SetColorsFromXaml(trueControl, TrueColor, "true");
                SetColorsFromXaml(falseControl, FalseColor, "false");

                trueControl.Opacity = 0;
                falseControl.Opacity = 0;
                if (NormalizedValue >= 0.5)
                {
                    trueControl.Visibility = Visibility.Visible;
                    falseControl.Visibility = Visibility.Collapsed;
                }
                else
                {
                    falseControl.Visibility = Visibility.Visible;
                    trueControl.Visibility = Visibility.Collapsed;
                }
                sb.Begin();
            }
        }

        /// <summary>
        /// Finds the true and false representaions and dets the
        /// gradient stop colors for them
        /// </summary>
        /// <param name="el"></param>
        /// <param name="colorPoint"></param>
        /// <param name="id"></param>
        private void SetColorsFromXaml(FrameworkElement el, ColorPoint colorPoint, string id)
        {
            if (el == null || colorPoint.HiColor == null || colorPoint.LowColor == null)
            {
                return;
            }

            GradientStop highStop = el.FindName(id+"HighColor") as GradientStop;
            GradientStop lowStop = el.FindName(id+"LowColor") as GradientStop;
            if (highStop != null && lowStop != null)
            {
                highStop.Color = colorPoint.HiColor;
                lowStop.Color = colorPoint.LowColor;
            }

        }

        /// <summary>
        /// A property has changed, if it is the Value property raise an IsTrue property changed event
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">event args</param>
        void PropertyHasChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                bool toBe = (NormalizedValue >= 0.5);
                if (IsTrue != toBe)
                {
                    SetValue(IsTrueProperty, toBe);
                }

            }
        }

        #endregion



    }
}
