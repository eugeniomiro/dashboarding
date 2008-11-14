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
    /// A Dial360  displays as a traditional circular guage with numbers from 0 to 100
    /// </summary>
    public partial class Dial360 : Dashboard
    {

        /// <summary>
        /// Constructs a Dial360
        /// </summary>
        public Dial360()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());

        }

        #region FaceColorRange property

        /// <summary>
        /// Our dependany property FaceColor has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(Dial360), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));

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
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void FaceColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dial360 instance = dependancy as Dial360;
            if (instance != null)
            {
                instance.SetFaceColor();
            }
        }


        #endregion

        #region NeedleColorRange property

        /// <summary>
        /// The NeedleColor Dependancy property
        /// </summary>
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(Dial360), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));

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
            Dial360 instance = dependancy as Dial360;
            if (instance != null)
            {
                instance.SetNeedleColor();
            }
        }

        #endregion

      
        /// <summary>
        /// Animate our Dial360. We calculate the needle position, color and the face color
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();
            
            _percentage.Text = String.Format("{0:000}", Value);
            //SetColourFromRange();
            double animateTo = -150 + (3 * Value);
            _needlePos.Value = animateTo;
            _moveNeedle.Begin();
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

        private void SetNeedleColor()
        {
            ColorPoint c = NeedleColorRange.GetColor(Value);
            if (c != null)
            {
                _needleHighColour.Color = c.HiColor;
                _needleLowColour.Color = c.LowColor;
            }
        }

        
    }
}
