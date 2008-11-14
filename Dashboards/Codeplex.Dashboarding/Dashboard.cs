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
    /// Common base class for all dashboard controls. Contains the Minimum, Maximum
    /// and current Value properties (Or at least it will eventually).
    /// 
    /// <para>Orchectrates initial render through the abstract render method</para>
    /// </summary>
    public abstract class Dashboard : UserControl
    {
        /// <summary>
        /// Constructs a Dashboard and initializes animation etc
        /// </summary>
        public Dashboard() :base()
        {
            Loaded += new RoutedEventHandler(Dashboard_Loaded);
            
        }


        /// <summary>
        /// Sub object loaded, let's animate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            Animate();
        }

        #region Value property

        /// <summary>
        /// Our dependany property Value has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", 
                                        typeof(double), typeof(Dashboard), 
                                        new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChanged)));

        /// <summary>
        /// Current value in the range Minimum <= Value <= Maximum
        /// </summary>
        public  double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set 
            { 
                SetValue(ValueProperty, value); 
             
            }
        }


        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ValuePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.Animate();
            }
        }


        #endregion

        /// <summary>
        /// Your value may have changed, please animate
        /// </summary>
        protected abstract void Animate();
    }
}
