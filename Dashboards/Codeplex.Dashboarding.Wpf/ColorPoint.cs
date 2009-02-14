﻿
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
using System.ComponentModel;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A color point represents the color of an item in 
    /// a color range. It consists of a blend low and high
    /// color and the value at which that color is used.
    /// 
    /// <para>This allow us to have a needle red at below 33%, yellow 
    /// up till 66% and green after that</para>
    /// </summary>
    public class ColorPoint: Control, INotifyPropertyChanged
    {
        /// <summary>
        /// Constructs a ColorPoint
        /// </summary>
        public ColorPoint()
        {

        }

        #region HiColor property

        /// <summary>
        /// The dependancy property for the HiColor attached property
        /// </summary>
        public static readonly DependencyProperty HiColorProperty =
            DependencyProperty.Register("HiColor", typeof(Color), typeof(ColorPoint), new PropertyMetadata(new PropertyChangedCallback(HiColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public Color HiColor
        {
            get { return (Color)GetValue(HiColorProperty); }
            set
            {
                SetValue(HiColorProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void HiColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint instance = dependancy as ColorPoint;
            if (instance != null)
            {              
                instance.OnPropertyChanged("HiColor");
            }
        }


        #endregion

        #region LowColor property

        /// <summary>
        /// The dependancy property for the LowColor attached property
        /// </summary>
        public static readonly DependencyProperty LowColorProperty =
            DependencyProperty.Register("LowColor", typeof(Color), typeof(ColorPoint), new PropertyMetadata(new PropertyChangedCallback(LowColorPropertyChanged)));

        /// <summary>
        /// low colour in the blend
        /// </summary>
        public Color LowColor
        {
            get { return (Color)GetValue(LowColorProperty); }
            set
            {
                SetValue(LowColorProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void LowColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint instance = dependancy as ColorPoint;
            if (instance != null)
            {
                instance.OnPropertyChanged("LowColor");
            }
        }



        #endregion

        #region Value property

        /// <summary>
        /// The dependancy property for Value attached property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ColorPoint), new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
           
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ValuePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint instance = dependancy as ColorPoint;
            if (instance != null)
            {
                instance.OnPropertyChanged("Value");
            }
        }


        #endregion


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged event if any one is listening
        /// </summary>
        /// <param name="prop">Name of the changed property</param>
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}