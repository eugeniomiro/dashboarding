
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

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// Common base class for all dashboard controls. Contains the Minimum, Maximum
    /// and current Value properties (Or at least it will eventually).
    /// 
    /// <para>Orchectrates initial render through the abstract render method</para>
    /// </summary>
    public abstract class Dashboard : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when the value changes
        /// </summary>
        public event EventHandler<DashboardValueChangedEventArgs> ValueChanged;

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
        /// dependancy property for the Value attached property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", 
                                        typeof(double), typeof(Dashboard), 
                                        new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChanged)));

        /// <summary>
        /// Current value in the range Minimum &lt;= Value &lt;= Maximum
        /// </summary>
        public  double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set 
            {
                double old = Value;
                SetValue(ValueProperty, value);
                OnValueChanged(old, value);
                OnPropertyChanged("Value");
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
