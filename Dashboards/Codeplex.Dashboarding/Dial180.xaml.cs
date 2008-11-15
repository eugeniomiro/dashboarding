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
    /// A Dial180 
    /// </summary>
    public partial class Dial180 : UserControl
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
        /// Dependancy property for the face color of our dial
        /// </summary>
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
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
        /// The NeedleColor Dependancy property
        /// </summary>
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
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
        /// Our dependany property has changed, deal with it
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
        /// The TextColor Dependancy property
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
        /// Our dependany property has changed, deal with it
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
        /// The dependancy property for theTextVisibilityiColor property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(Dial180), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        /// <summary>
        /// Show or hide the text
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
            Dial180 instance = dependancy as Dial180;


            if (instance != null)
            {
                instance._text.Visibility = instance.TextVisibility;
            }
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public  double Value 
        {
            get { return _t; }
            set { _t = value; Animate(); }
        }
        private double _t;

        void Animate()
        {
            SetFaceColor();
            SetNeedleColor();
            _text.Text = "" + Value;
            double point = -90 + ((Value / 100) * 180);
            _value.Value = point;
            _swipe.Begin();
        }

    }
}
