
#region Copyright 2008 David Black

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

#endregion


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
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// Common base class for all dashboard controls. Contains the Minimum, Maximum
    /// and current Value properties (Or at least it will eventually).
    /// 
    /// <para>Orchectrates initial render through the abstract render method</para>
    /// </summary>
    public abstract class Dashboard : PlatformIndependentDashboard, INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when the value changes
        /// </summary>
        public event EventHandler<DashboardValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Constructs a Dashboard and initializes animation etc, defaults are
        /// Minimun = 0, Maximum = 100, AnimationDuration = 0.75 seconds
        /// </summary>
        protected Dashboard() :base()
        {
            Minimum = 0;
            Maximum = 100;
            AnimationDuration = TimeSpan.FromSeconds(0.75);
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
        /// dependancy property for the Value attached property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", 
                                        typeof(double), typeof(Dashboard), 
                                        new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChanged)));

        /// <summary>
        /// Current value in the range Minimum &lt;= Value &lt;= Maximum
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public double Value
        {
            // suppressed message because GetValue is from dependancyProperty and too late to rename value
            get 
            { 
                return (double)GetValue(ValueProperty); 
            }
            set 
            {
                double old = Value;
                
                if (value > RealMaximum)
                {
                    value = RealMaximum;
                }

                if (value < RealMinimum)
                {
                    value = RealMinimum;
                }

                SetValue(ValueProperty, value);
                OnValueChanged(old, value);

            }
        }

        /// <summary>
        /// The value has changed, if anyone is listening let em know
        /// </summary>
        /// <param name="old">Initial value</param>
        /// <param name="value">New value</param>
        private void OnValueChanged(double old, double value)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new DashboardValueChangedEventArgs { OldValue  = old, NewValue = value});
            }
        }


        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ValuePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {

                double v = instance.Value;
                if (v > instance.RealMaximum)
                {
                    instance.Value = instance.RealMaximum;
                }

                if (v < instance.RealMinimum)
                {
                    instance.Value = instance.RealMinimum;
                }
                instance.OnPropertyChanged("Value");
                instance.Animate();
            }
        }

      


        #endregion


        #region AnimationDuration property



        /// <summary>
        /// Gets or sets the duration of the animation. The default is 0.75 seconds. Set
        /// the animation duration depending on the interval between the value changing.
        /// </summary>
        /// <value>The duration of the animation.</value>
        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(AnimationDurationChanged)));

        /// <summary>
        /// The animation duration dependancy property changed. We don't do alot as we
        /// pick this up at the next render
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void AnimationDurationChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
             Dashboard instance = dependancy as Dashboard;
             if (instance != null)
             {

             }
        }


        #endregion



        #region min max functions

        #region Minimum property

        /// <summary>
        /// Dependancy property backing the Minimum value
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(MinimumPropertyChanged)));

        /// <summary>
        /// Minimum value the gauge will take, values lower than this are raised to this
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty);  }
            set { SetValue(MinimumProperty, value); }
        }
        
        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MinimumPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("Minimum");
            }
        }

      

        #endregion

        #region MaximumProperty

        /// <summary>
        /// Dependancy property backing the Maximim value 
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(MaximumPropertyChanged)));

        /// <summary>
        /// Maximum value the gauge will tae. Values above this are rounded down
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value);  }
        }

                /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MaximumPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("Maximum");
            }
        }


        #endregion


        #region RealMaximum property

        /// <summary>
        /// Since the user may set Minimum > Maximum, we internally use RealMinimum
        /// and RealMaximum which allways return Maximum >= Minimum even if they have to swap
        /// </summary>
        internal double RealMaximum 
        {
            get
            {
                return (Maximum > Minimum) ? Maximum : Minimum;
            }
        }

        #endregion

        #region RealMaximum property

        /// <summary>
        /// Since the user may set Minimum > Maximum, we internally use RealMinimum
        /// and RealMaximum which always return Maximum >= Minimum even if they have to swap
        /// </summary>
        internal double RealMinimum
        {
            get
            {
                return (Maximum < Minimum) ? Maximum : Minimum;
            }
        }

        #endregion

        #region Normalised Value

        /// <summary>
        /// Rgardless of the Minimum or Maximum settings, the actual value to display on the gauge
        /// is represented by the Normalized value which always is in the range 0 &gt;= n &lt;= 1.0. This makes
        /// the job of animating more easy.
        /// </summary>
        internal double NormalizedValue
        {
            get
            {
                double range = RealMaximum - RealMinimum;
                return (Value- RealMinimum) /range;
            }
        }


        #endregion

        #endregion

        /// <summary>
        /// Instructs derived classes that the Value property
        /// has changed and that they should update their appearance appropriately
        /// </summary>
        protected abstract void Animate();

      

        #region INotifyPropertyChanged Members

        /// <summary>
        /// One of my properties has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify any listeners that a property has changed
        /// </summary>
        /// <param name="propName">Name of the property</param>
        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion


    }

    /// <summary>
    /// Event args for the ValueChangedEvent
    /// </summary>
    public class DashboardValueChangedEventArgs: EventArgs
    {
        /// <summary>
        /// Value of the dashboard prior to the change
        /// </summary>
        public double OldValue { get; set; }

        /// <summary>
        /// new value of the dashboard
        /// </summary>
        public double NewValue { get; set; }


    }

}
