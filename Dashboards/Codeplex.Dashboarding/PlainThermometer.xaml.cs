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
    public partial class PlainThermometer : Dashboard
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
            SetMercuryColor();
            _translate.Value = _fullTranslate * (Value / 100);
            _scale.Value = _fullScale * (Value / 100);
            _text.Text = "" + Value;
            _swipe.Begin();
        }
        #endregion
    }
}
