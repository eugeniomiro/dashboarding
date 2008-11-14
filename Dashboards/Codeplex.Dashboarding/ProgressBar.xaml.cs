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
    /// A progress bar show the 0 .. 100% progress of an operation
    /// </summary>
    public partial class ProgressBar : Dashboard
    {
        /// <summary>
        /// Constructs a ProgressBar
        /// </summary>
        public ProgressBar()
        {
            InitializeComponent();
        }

        #region OutlineColor property
        /// <summary>
        /// The dependancy color for the OutlineColor property
        /// </summary>
        public static readonly DependencyProperty OutlineColorProperty =
            DependencyProperty.Register("OutlineColor", typeof(Color), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public Color OutlineColor
        {
            get { return (Color)GetValue(OutlineColorProperty); }
            set
            {
                SetValue(OutlineColorProperty, value);
            }
        }

        /// <summary>
        /// One of our dependany properties have changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;


            if (instance != null)
            {
                if (instance.OutlineColor != null)
                {
                    instance._grid.Stroke = new SolidColorBrush(instance.OutlineColor);
                    instance._coloured.Stroke = new SolidColorBrush(instance.OutlineColor);
                    instance._gray.Stroke = new SolidColorBrush(instance.OutlineColor);
                }
            }
        }

        #endregion




        #region InProgressColor property

        /// <summary>
        /// Dependancy Property for the InProgressColor property
        /// </summary>
        public static readonly DependencyProperty InProgressColorProperty =
            DependencyProperty.Register("InProgressColor", typeof(ColorPoint), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(InProgressColorPropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        public ColorPoint InProgressColor
        {
            get
            {
                
                ColorPoint res = (ColorPoint)GetValue(InProgressColorProperty);
                return res;
            }
            set
            {
                SetValue(InProgressColorProperty, value);
                Animate();
            }
        }



        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void InProgressColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;
            if (instance != null)
            {
                instance.SetInProgressColor();
            }
        }

        private void SetInProgressColor()
        {
            _highEnabled0.Color = InProgressColor.HiColor;
            
            _lowEnabled0.Color = InProgressColor.LowColor;
        }


        #endregion

        #region OutOfProgressColor property

        /// <summary>
        /// Dependancy Property for the OutOfProgressColor
        /// </summary>
        public static readonly DependencyProperty OutOfProgressColorProperty =
            DependencyProperty.Register("OutOfProgressColor", typeof(ColorPoint), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(OutOfProgressColorPropertyChanged)));

        /// <summary>
        /// Sets the color range for when the value is false. Please see the definition of
        /// TrueColor range for a vacuous example of when ths may be needed
        /// </summary>
        public ColorPoint OutOfProgressColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(OutOfProgressColorProperty);
                return res;
            }
            set
            {
                SetValue(OutOfProgressColorProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void OutOfProgressColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            ProgressBar instance = dependancy as ProgressBar;
            if (instance != null)
            {
                instance.SetOutOfProgressColor();
            }
        }

        private void SetOutOfProgressColor()
        {
            _highDisabled0.Color = OutOfProgressColor.HiColor;
           

            _lowDisabled0.Color = OutOfProgressColor.LowColor;
           

        }

        #endregion





        /// <summary>
        /// Calculate the destination position of the animation for the current value
        /// and set it off
        /// </summary>
        protected override void Animate()
        {
            _startPoint.To = new Point(Value, 0);
            _topLeft.To = new Point(Value, 0);
            _botLeft.To = new Point(Value, 15);
            _swipe.Begin();
        }
    }
}
