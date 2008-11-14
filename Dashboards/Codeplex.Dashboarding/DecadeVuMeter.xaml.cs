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

        private void SetDefaultColours()
        {
            LedOffColor = Color.FromArgb(0xFF, 0x26, 0x41, 0x08);
            LedOnColor = Color.FromArgb(0xFF, 0x96, 0xfb, 0x23);
            BorderColor = Color.FromArgb(0xFF, 0x27, 0x53, 0x18);
            _text.Foreground = new SolidColorBrush(LedOnColor);
        }


        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {
            _text.Text = Value.ToString();
            for (int i = 0; i < NumberOfLeds; i++)
            {
                Storyboard sb = LayoutRoot.FindName("TimelineLed" + (NumberOfLeds - (i + 1))) as Storyboard;



                if (sb != null)
                {
                    double pos = ((i + 1) / (double)NumberOfLeds) * 100;
                    if (Value >= pos)
                    {
                        sb.Begin();
                    }
                    else
                    {
                        sb.Seek(new TimeSpan(0, 0, 0));
                        sb.Stop();
                    }
                }
            }
        }

        #region BorderColor property
        /// <summary>
        /// The dependancy color for the BorderColor property
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)) );

        /// <summary>
        /// Hi colour in the blend
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
        /// Our dependany property has changed, deal with it
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
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty LedOnColorProperty =
            DependencyProperty.Register("LedOnColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
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
        /// THe dependancy property for the LedOffColor
        /// </summary>
        public static readonly DependencyProperty LedOffColorProperty =
            DependencyProperty.Register("LedOffColor", typeof(Color), typeof(DecadeVuMeter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
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


    }
}
