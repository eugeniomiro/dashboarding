﻿using System;
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
    /// A diamond slider it a progress type gauge where a diamond shaped
    /// indicator is moved across a background rectangle.
    /// <para>The following can be customized
    /// <list type="bullet">
    /// <item>DiamondColor</item>
    /// <item>Progress bar gradient points (left, mid and right></item>
    /// </list>
    /// </para>
    /// </summary>
    public partial class DiamondSlider : Dashboard
    {
        /// <summary>
        /// Constructs a DiamondSlider
        /// </summary>
        public DiamondSlider()
        {
            InitializeComponent();
        }

        #region DiamondColorproperty
        /// <summary>
        /// The dependancy color for the DiamondColorproperty
        /// </summary>
        public static readonly DependencyProperty DiamondColorProperty =
            DependencyProperty.Register("DiamondColor", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(DiamondColorPropertyChanged)));

        /// <summary>
        /// Color of the Diamond
        /// </summary>
        public Color DiamondColor
        {
            get { return (Color)GetValue(DiamondColorProperty); }
            set
            {
                SetValue(DiamondColorProperty, value);
            }
        }

        /// <summary>
        /// DiamondColor dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void DiamondColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance._slider.Fill = new SolidColorBrush(instance.DiamondColor);
                }
            }
        }

        #endregion

        #region LeftGradientproperty
        /// <summary>
        /// The dependancy color for the LeftGradientproperty
        /// </summary>
        public static readonly DependencyProperty LeftGradientProperty =
            DependencyProperty.Register("LeftGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(LeftGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the left hand side
        /// </summary>
        public Color LeftGradient
        {
            get { return (Color)GetValue(LeftGradientProperty); }
            set
            {
                SetValue(LeftGradientProperty, value);
            }
        }

        /// <summary>
        /// LeftGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void LeftGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance._leftColor.Color = instance.LeftGradient;
                }
            }
        }

        #endregion

        #region MidGradientproperty
        /// <summary>
        /// The dependancy color for the MidGradientproperty
        /// </summary>
        public static readonly DependencyProperty MidGradientProperty =
            DependencyProperty.Register("MidGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(MidGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the mid point
        /// </summary>
        public Color MidGradient
        {
            get { return (Color)GetValue(MidGradientProperty); }
            set
            {
                SetValue(MidGradientProperty, value);
            }
        }

        /// <summary>
        /// MidGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MidGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance._midColor.Color = instance.MidGradient;
                }
            }
        }

        #endregion

        #region RightGradientproperty
        /// <summary>
        /// The dependancy color for the RightGradientproperty
        /// </summary>
        public static readonly DependencyProperty RightGradientProperty =
            DependencyProperty.Register("RightGradient", typeof(Color), typeof(DiamondSlider), new PropertyMetadata(new PropertyChangedCallback(RightGradientPropertyChanged)));

        /// <summary>
        /// Color of the gradient fill at the right hand side
        /// </summary>
        public Color RightGradient
        {
            get { return (Color)GetValue(RightGradientProperty); }
            set
            {
                SetValue(RightGradientProperty, value);
            }
        }

        /// <summary>
        /// RightGradient dependancy property changed, update the instance
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void RightGradientPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            DiamondSlider instance = dependancy as DiamondSlider;


            if (instance != null)
            {
                if (instance._slider != null)
                {
                    instance._rightColor.Color = instance.RightGradient;
                }
            }
        }

        #endregion

        /// <summary>
        /// Moves the diamond
        /// </summary>
        protected override void Animate()
        {
            _animValue.Value = Value;
            _swipe.Begin();
        }
    }
}
