﻿//-----------------------------------------------------------------------
// <copyright file="Dashboard.cs" company="David Black">
//      Copyright 2008 David Black
//  
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at
//     
//          http://www.apache.org/licenses/LICENSE-2.0
//    
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace Codeplex.Dashboarding
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Common base class for all dashboard controls. Contains the Minimum, Maximum
    /// and current Value properties (Or at least it will eventually).
    /// <para>Orchectrates initial render through the abstract render method</para>
    /// </summary>
    public abstract class Dashboard : PlatformIndependentDashboard, INotifyPropertyChanged
    {
        #region public static fields
        /// <summary>
        /// dependancy property for the Value attached property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChanged)));
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(AnimationDurationChanged)));

        /// <summary>
        /// Dependancy property backing the Minimum value
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(MinimumPropertyChanged)));

        /// <summary>
        /// Dependancy property backing the Maximim value 
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(MaximumPropertyChanged)));

        /// <summary>
        /// The Dependancy property for the TextColor attached property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(TextColorChanged)));

        /// <summary>
        /// The dependancy property for theTextVisibility attached property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        /// <summary>
        /// The dependancy property for TextFormat attached property
        /// </summary>
        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register("TextFormat", typeof(string), typeof(Dashboard), new PropertyMetadata(new PropertyChangedCallback(TextFormatPropertyChanged)));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Dashboard"/> class and initializes animation etc, defaults are
        /// Minimum = 0, Maximum = 100, AnimationDuration = 0.75 seconds
        /// </summary>
        protected Dashboard()
            : base()
        {
            this.Minimum = 0;
            this.Maximum = 100;
            this.TextFormat = "{0:000}";
            this.AnimationDuration = TimeSpan.FromSeconds(0.75);
            Loaded += new RoutedEventHandler(this.Dashboard_Loaded);
        }

        #region events

        /// <summary>
        /// Occurs when the value changes.
        /// </summary>
        public event EventHandler<DashboardValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the value. The value lies in the range Minimum &lt;= Value &lt;= Maximum
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Value is to engrained to change and really Microsoft should be shot for calling the dependancy property set method SetVale and GetValue since Value is a very common property name")]
        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }

            set
            {
                double old = this.Value;

                if (value > this.RealMaximum)
                {
                    value = this.RealMaximum;
                }

                if (value < this.RealMinimum)
                {
                    value = this.RealMinimum;
                }

                SetValue(ValueProperty, value);
                this.OnValueChanged(old, value);
                this.OnPropertyChanged("FormattedValue");
            }
        }

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
        /// Gets or sets the Minimum value the gauge will take, values lower than this are raised to this
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value the gauge will tae. Values above this are rounded down
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text used to display the value.
        /// </summary>
        /// <value>The color of the text.</value>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
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
            }
        }

        /// <summary>
        /// Gets or sets a the visibility of the textural representation of the value
        /// </summary>
        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets a the format string of the textural representation of the value. Since
        /// the default is 0..100 we use {0:000} as the default format string.
        /// <para>
        ///     The format of the string is a String.Format specifier. You should consume parameter
        ///     zero {0} as the place holder for the text representation of the Value.  
        /// </para>
        /// <para>
        /// Valid options are
        /// <list type="bullet">
        /// <item><code>"{0}"</code> - Unstructured, value will over flow all over the place</item>
        /// <item><code>"{0:000}"</code> three digits padded e.g. 032</item>
        /// <item><code>"{0:00}c"</code> - a temperatre two digits and a 'c' ie 23c</item>
        /// <item><code>"{0:00.0}f"</code> - a temperatre: two digits, a decimal digit and an 'f' ie 23.6f</item>
        /// </list>
        /// </para>
        /// </summary>
        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        /// <summary>
        /// Gets the textural representation of the value as specified through the TextFormat property.
        /// </summary>
        /// <value>The formatted value. If the TextFormat property null we return the value formatted
        /// using "{0:000} which is the backwards compatabilty value. If TextFormat 
        /// is not a valid format string we return "???" rather than crashing
        /// </value>
        public string FormattedValue
        {
            get 
            {
                try
                {
                    return String.Format(this.TextFormat ?? "{0:000}", this.Value);
                }
                catch (FormatException)
                {
                    return "???";
                }
            }
        }

        #endregion

        #region internalproperties

        /// <summary>
        /// Gets the RealMinimum value. Since the user may set Minimum &gt; Maximum, we internally use RealMinimum
        /// and RealMaximum which allways return Maximum &gt;= Minimum even if they have to swap
        /// </summary>
        internal double RealMaximum
        {
            get { return (this.Maximum > this.Minimum) ? this.Maximum : this.Minimum; }
        }

        /// <summary>
        /// Gets the RealMaximum valueSince the user may set Minimum &gt; Maximum, we internally use RealMinimum
        /// and RealMaximum which always return Maximum &gt;= Minimum even if they have to swap
        /// </summary>
        internal double RealMinimum
        {
            get { return (this.Maximum < this.Minimum) ? this.Maximum : this.Minimum; }
        }

        /// <summary>
        /// Gets the NormalizedValue. Regardless of the Minimum or Maximum settings, the actual value to display on the gauge
        /// is represented by the Normalized value which always is in the range 0 &gt;= n &lt;= 1.0. This makes
        /// the job of animating more easy.
        /// </summary>
        internal double NormalizedValue
        {
            get
            {
                double range = this.RealMaximum - this.RealMinimum;
                return (this.Value - this.RealMinimum) / range;
            }
        }

        #endregion

        /// <summary>
        /// Instructs derived classes that the Value property
        /// has changed and that they should update their appearance appropriately
        /// </summary>
        protected abstract void Animate();

        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected abstract void UpdateTextColor();

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected abstract void UpdateTextVisibility();

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected abstract void UpdateTextFormat();

        /// <summary>
        /// Notify any listeners that a property has changed
        /// </summary>
        /// <param name="propName">Name of the property</param>
        protected virtual void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #region private static methods

        /// <summary>
        /// The Value property changed, raise the property changed event for INotifyPropertyChange 
        /// an update the controls visual state
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
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
            
        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MinimumPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("Minimum");
            }
        }
        
        /// <summary>
        /// Our dependany property has changed, deal with it and ensure the Property change notification 
        /// of INotifyPropertyChanges is triggered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MaximumPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.OnPropertyChanged("Maximum");
            }
        }

        #endregion
        
        #region private methods

        /// <summary>
        /// The Text visibility property has cahaned..
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.UpdateTextVisibility();
                instance.OnPropertyChanged("TextVisibility");
            }
        }

        /// <summary>
        /// The text color dependancy property changed, deal with it
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        private static void TextColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                instance.UpdateTextColor();
                instance.OnPropertyChanged("TextColor");
            }
        }

        /// <summary>
        /// The format string representing the value display format has changed
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void TextFormatPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Dashboard instance = dependancy as Dashboard;
            if (instance != null)
            {
                 instance.UpdateTextFormat();
                instance.OnPropertyChanged("TextFormat");
            }
        }

        /// <summary>
        /// The value has changed, if anyone is listening let em know
        /// </summary>
        /// <param name="old">The initial value</param>
        /// <param name="value">The new value</param>
        private void OnValueChanged(double old, double value)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new DashboardValueChangedEventArgs { OldValue = old, NewValue = value });
            }
        }

        /// <summary>
        /// Handles the Loaded event of the Dashboard control. This
        /// will trigger the first call to Animate()
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            this.Animate();
        }
        #endregion
    }
}
