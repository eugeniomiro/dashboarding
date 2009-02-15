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
    /// Intended to look like a Vu meter from an old cassette deck this thermomter style
    /// control displays data on blocks rather than as a continious sweep.
    /// </summary>
    public partial class DecadeVuMeter : Dashboard
    {
        private const int NumberOfLeds = 10;

        /// <summary>
        /// Constructs a DecadeVduMeter
        /// </summary>
        public DecadeVuMeter()
        {
            InitializeComponent();
            SetDefaultColours();
            SetTimerDelay();
        }

        #region initialize
        /// <summary>
        /// Sets the default colors into the attached properties
        /// </summary>
        private void SetDefaultColours()
        {
            LedOffColor = Color.FromArgb(0xFF, 0x26, 0x41, 0x08);
            LedOnColor = Color.FromArgb(0xFF, 0x96, 0xfb, 0x23);
            BorderColor = Color.FromArgb(0xFF, 0x27, 0x53, 0x18);
            _text.Foreground = new SolidColorBrush(LedOnColor);
        }
        #endregion

        #region Animation
        /// <summary>
        /// Display the control according the the current value. That means
        /// lighting the necessary LEDS
        /// </summary>
        protected override void Animate()
        {
            _text.Text = Value.ToString();
            for (int i = 0; i < NumberOfLeds; i++)
            {
                Storyboard sb = GetStoryBoard("TimelineLed" + (NumberOfLeds - (i + 1))) as Storyboard;
                if (sb != null)
                {
                    double pos = ((i + 1) / (double)NumberOfLeds) * 100;
                    if ((NormalizedValue * 100) >= pos)
                    {
                        Start(sb);
                    }
                    else
                    {
                       
                        sb.Stop();
                        sb.Seek(new TimeSpan(0, 0, 0));
                    }
                }
            }
        }
        #endregion

        #region BorderColor property
        /// <summary>
        /// The dependancy property for the BorderColor attached property
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)) );

        /// <summary>
        /// Color of the border around each led in the control
        /// </summary>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        /// <summary>
        /// BorderColor property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DecadeVuMeter instance = dependancy as DecadeVuMeter;


            if (instance != null)
            {
                if (instance.LedOnColor != null)
                {
                    instance._text.Foreground = new SolidColorBrush(instance.LedOnColor);
                }
                for (int i = 0; i < NumberOfLeds; i++)
                {
                    instance.SetLedColours(i);
                }
            }
        }

        #endregion

        #region LedOnColor property
        /// <summary>
        /// The dependancy property for the LedOn attached property
        /// </summary>
        public static readonly DependencyProperty LedOnColorProperty =
            DependencyProperty.Register("LedOnColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// The LedOnColor is the color of a lit LED
        /// </summary>
        public Color LedOnColor
        {
            get { return (Color)GetValue(LedOnColorProperty); }
            set
            {
                SetValue(LedOnColorProperty, value);
            }
        }

        #endregion

        #region LedOffColor property

        /// <summary>
        /// THe dependancy property for the LedOffColor attached property
        /// </summary>
        public static readonly DependencyProperty LedOffColorProperty =
            DependencyProperty.Register("LedOffColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Color of the LED when not lit
        /// </summary>
        public Color LedOffColor
        {
            get { return (Color)GetValue(LedOffColorProperty); }
            set
            {
                SetValue(LedOffColorProperty, value);
            }
        }

        #endregion

        #region initialize timers

        /// <summary>
        /// In order to ripple the value up we claculate the time
        /// to on for each block
        /// </summary>
        private void SetTimerDelay()
        {
            int step = 0;
                for (int i =0; i < NumberOfLeds; i++)
                {
                SetLedColours(i);

                double time = ((double)NumberOfLeds - i) / (double)NumberOfLeds;

                int endMs = (int)(100 * time);
                int startMs = (int)(100 * (time - (1 / (double)NumberOfLeds)));

                SplineColorKeyFrame start = LayoutRoot.FindName("_startColour" + i) as SplineColorKeyFrame;
                SplineColorKeyFrame end = LayoutRoot.FindName("_endColour" + i) as SplineColorKeyFrame;
                if (end != null && start != null)
                {
                    start.SetValue(SplineColorKeyFrame.KeyTimeProperty, KeyTime.FromTimeSpan( new TimeSpan(0, 0, 0, 0, startMs)));
                    end.SetValue(SplineColorKeyFrame.KeyTimeProperty, KeyTime.FromTimeSpan( new TimeSpan(0, 0, 0, 0, endMs)));
                }
            }
        }

        /// <summary>
        /// Sets the color according to On or off of a single LED
        /// </summary>
        /// <param name="i"></param>
        private void SetLedColours(int i)
        {
            SplineColorKeyFrame start = LayoutRoot.FindName("_startColour" + i) as SplineColorKeyFrame;
            if (start != null)
            {
                start.Value = LedOffColor;
            }
            SplineColorKeyFrame end = LayoutRoot.FindName("_endColour" + i) as SplineColorKeyFrame;
            if (end != null)
            {
                end.Value = LedOnColor;
            }
            Rectangle led = LayoutRoot.FindName("_led" + i) as Rectangle;
            if (led != null)
            {
                led.Stroke = new SolidColorBrush(BorderColor);
                led.Fill = new SolidColorBrush(LedOffColor);
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
