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
    /// Common code for all binary Dashboard controls.
    /// </summary>
    public abstract class BinaryDashboard: Dashboard
    {
        /// <summary>
        /// Constructs a BinaryDashboard
        /// </summary>
        public BinaryDashboard() :base()
        {
            
            SetValue(TrueColorProperty, new ColorPoint { HiColor=Color.FromArgb(0xFF,0x6C,0xFA,0x20), LowColor = Color.FromArgb(0xFF,0xDC,0xF9,0xD4) });
            SetValue(FalseColorProperty, new ColorPoint { HiColor = Color.FromArgb(0xFF, 0xFA, 0x65, 0x65), LowColor = Color.FromArgb(0xFF, 0xFC, 0xD5, 0xD5) });   
        }

        /// <summary>
        /// Show or hised the correct element depending on the state and then starts
        /// any animation associated with the value
        /// </summary>
        /// <param name="trueControl">FrameWorkElement representing a true value</param>
        /// <param name="falseControl">FrameWorkElement representing false</param>
        /// <param name="sb">Storyboard to start</param>
        protected void PerformCommonBinaryAnimation(FrameworkElement trueControl, FrameworkElement falseControl, Storyboard sb)
        {
            if (trueControl != null || falseControl != null || sb != null)
            {
                SetColorsFromXaml(trueControl, TrueColor, "true");
                SetColorsFromXaml(falseControl, FalseColor, "false");

                trueControl.Opacity = 0;
                falseControl.Opacity = 0;
                if (Value >= 50)
                {
                    trueControl.Visibility = Visibility.Visible;
                    falseControl.Visibility = Visibility.Collapsed;
                }
                else
                {
                    falseControl.Visibility = Visibility.Visible;
                    trueControl.Visibility = Visibility.Collapsed;
                }
                sb.Begin();
            }
        }

        private void SetColorsFromXaml(FrameworkElement el, ColorPoint colorPoint, string id)
        {
            if (el == null || colorPoint.HiColor == null || colorPoint.LowColor == null)
            {
                return;
            }

            GradientStop highStop = el.FindName(id+"HighColor") as GradientStop;
            GradientStop lowStop = el.FindName(id+"LowColor") as GradientStop;
            if (highStop != null && lowStop != null)
            {
                highStop.Color = colorPoint.HiColor;
                lowStop.Color = colorPoint.LowColor;
            }


        }




        #region TrueColor property

        /// <summary>
        /// Our dependany property TrueColor has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        public static readonly DependencyProperty TrueColorProperty =
            DependencyProperty.Register("TrueColor", typeof(ColorPoint), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(TrueColorPropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        public ColorPoint TrueColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(TrueColorProperty);
                return res;
            }
            set
            {
                SetValue(TrueColorProperty, value);
                Animate();
            }
        }



        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TrueColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                instance.Animate();
            }
        }


        #endregion

        #region FalseColor property

        /// <summary>
        /// Our dependany property FalseColor has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        public static readonly DependencyProperty FalseColorProperty =
            DependencyProperty.Register("FalseColor", typeof(ColorPoint), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(FalseColorPropertyChanged)));

        /// <summary>
        /// Sets the color range for when the value is false. Please see the definition of
        /// TrueColor range for a vacuous example of when ths may be needed
        /// </summary>
        public ColorPoint FalseColor
        {
            get
            {
                ColorPoint res = (ColorPoint)GetValue(FalseColorProperty);
                return res;
            }
            set
            {
                SetValue(FalseColorProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void FalseColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                instance.Animate();
            }
        }

        #endregion


        #region IsTrue property

        /// <summary>
        /// Our dependany property 
        /// </summary>
        public static readonly DependencyProperty IsTrueProperty =
            DependencyProperty.Register("IsTrue", typeof(Boolean), typeof(BinaryDashboard), new PropertyMetadata(new PropertyChangedCallback(IsTruePropertyChanged)));

        /// <summary>
        /// The colour range for the boolean indicator when the underlying value is true.
        /// Note in some instances (in english) true is good (green) in some circumstances
        /// bad (red). Hearing a judge say Guilty to you would I think be 
        /// a red indicator for true :-)
        /// </summary>
        public Boolean IsTrue
        {
            get
            {
                Boolean res = (Boolean)GetValue(IsTrueProperty);
                return res;
            }
            set
            {
                SetValue(IsTrueProperty, value);
                Animate();
            }
        }



        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void IsTruePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BinaryDashboard instance = dependancy as BinaryDashboard;
            if (instance != null)
            {
                double value = (instance.IsTrue) ? 100 : 0;
                instance.SetValue(BinaryDashboard.ValueProperty, value);
            }
        }


        #endregion



    }
}
