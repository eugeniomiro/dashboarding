﻿/* -------------------------------------------------------------------------
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
    /// A needle and dial face control where the needle sweeps a 180 degree path. 
    /// </summary>
    public partial class Dial180 : Dashboard
    {
        /// <summary>
        /// constructs a dial 180 
        /// </summary>
        public Dial180()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());

        }


        #region FaceColorRange property

        /// <summary>
        /// Dependancy property for the FaceColor attached property
        /// </summary>
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));

        /// <summary>
        /// Specifies the face color at points in the range. A single color point with
        /// a value of 0 specifies the color for all
        /// </summary>
        public ColorPointCollection FaceColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(FaceColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(FaceColorRangeProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, update the face color
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void FaceColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dial180 instance = dependancy as Dial180;
            if (instance != null)
            {
                instance.SetFaceColor();
            }
        }

        /// <summary>
        /// Sets the face color from the color range
        /// </summary>
        private void SetFaceColor()
        {

            ColorPoint c = FaceColorRange.GetColor(Value);
            if (c != null)
            {
                _colourRangeStart.Color = c.HiColor;
                _colourRangeEnd.Color = c.LowColor;
            }
        }

        #endregion

        #region NeedleColorRange property

        /// <summary>
        /// The  Dependancy property for the NeedleColor attached property
        /// </summary>
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));

        /// <summary>
        /// Specifies what color the needle is a various point is the range
        /// </summary>
        public ColorPointCollection NeedleColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(NeedleColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(NeedleColorRangeProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our needle color has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void NeedleColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dial180 instance = dependancy as Dial180;
            if (instance != null)
            {
                instance.SetNeedleColor();
            }
        }

        /// <summary>
        /// Sets the needle color from the color range
        /// </summary>
        private void SetNeedleColor()
        {
            ColorPoint c = NeedleColorRange.GetColor(Value);
            if (c != null)
            {
                _needleHighColour.Color = c.HiColor;
                _needleLowColour.Color = c.LowColor;
            }
        }

        #endregion

        #region TextColor
       
        /// <summary>
        /// The Dependancy property for the TextColor attached property
        /// </summary>
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(TextColorChanged)));

        /// <summary>
        /// The color of the text used to show the percentage
        /// </summary>
        public Color TextColor
        {
            get
            {
                Color res = (Color)GetValue(TextColorProperty);
                return res;
            }
            set
            {
                SetValue(TextColorProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our TextColor dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dial180 instance = dependancy as Dial180;
            if (instance != null)
            {
                instance._text.Foreground = new SolidColorBrush(instance.TextColor);
            }
        }

        #endregion

        #region TextVisibility property
        /// <summary>
        /// The dependancy property for theTextVisibility attached property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        /// <summary>
        /// Show or hide the text according to the visibility
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
        /// Our TextVivibility dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dial180 instance = dependancy as Dial180;


            if (instance != null)
            {
                instance._text.Visibility = instance.TextVisibility;
            }
        }

        #endregion




        /// <summary>
        /// Move the needle and set the needle and face colors to suite the value
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();
            _text.Text = "" + Value;
            double point = -90 + (NormalizedValue * 180);
            _value.Value = point;
            _swipe.Begin();

        }
    }
}
