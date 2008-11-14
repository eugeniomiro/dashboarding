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
    /// A color point represents the color of an item in 
    /// a color range. It consists of a blend low and high
    /// color and the value at which that color is used.
    /// 
    /// <para>This allow us to have a needle red at below 33%, yellow 
    /// up till 66% and green after that</para>
    /// <para>We derive from control because we can't from dependancy control</para>
    /// </summary>
    public class ColorPoint: Control
    {
        /// <summary>
        /// Constructs a ClorPoint
        /// </summary>
        public ColorPoint()
        {

        }

        

        #region HiColor property

        /// <summary>
        /// The dependancy property for the HiColor property
        /// </summary>
        public static readonly DependencyProperty HiColorProperty =
            DependencyProperty.Register("HiColor", typeof(Color), typeof(ColorPoint), null);

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

        #endregion

        #region LowColor property

        /// <summary>
        /// The dependancy property for the LowColor property
        /// </summary>
        public static readonly DependencyProperty LowColorProperty =
            DependencyProperty.Register("LowColor", typeof(Color), typeof(ColorPoint), null);

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

        #endregion

        #region Value property

        /// <summary>
        /// The dependancy property for Value as a property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ColorPoint), null);

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

        #endregion

    }
}
