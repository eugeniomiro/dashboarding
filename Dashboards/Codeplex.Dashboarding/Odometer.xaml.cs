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
    /// Provides a simple Odometer. Based on the technique  shown at: http://www.agavegroup.com/?p=60
    /// 
    /// <para>This control can be used as a standard page count style Odometer and also
    /// as a games Hi Score control</para>
    /// 
    /// </summary>
    public partial class Odometer : UserControl
    {

        #region private members

        /// <summary>
        /// storyboard to act as a timer
        /// </summary>
        Storyboard _timer = new Storyboard();

        /// <summary>
        /// All digits for the number in the order most significant to least
        /// </summary>
        private List<OdometerDigit> _digits = new List<OdometerDigit>();

        #endregion

        #region Mode enum
        /// <summary>
        /// The type of update required
        /// </summary>
        public enum Mode { 
            /// <summary>
            /// Start from InitialValue and add one every Interval Seconds
            /// </summary>
            AutoIncrement, 
            
            /// <summary>
            /// Start from InitialValue and subtract one every Interval Seconds
            /// </summary>
            AutoDecrement, 
            
            /// <summary>
            /// Start from initial value and increment or decrement until we reach FinalValue then stop
            /// </summary>
            InitialToFinal, 
            
            /// <summary>
            /// Initialize all digits to a staic value and leave well alone. In future we might
            /// have a initial value animation to make this less boring.
            /// </summary>
            Static 
        };

        #endregion

        #region IncrementDigit enum
        /// <summary>
        /// Used to specifc which digit within the Odometer the user wishes
        /// to manipulate when directly Incrementing or Decrementing
        /// </summary>
        public enum IncrementDecrementDigit
        {

            /// <summary>
            /// Increment the value by 1, the control will automatically roll over if necessary
            /// </summary>
            Units = 1,

            /// <summary>
            /// Increment the value by 10, the control will automatically roll over if necessary
            /// </summary>
            Tens = 2,

            /// <summary>
            /// Increment the value by 100, the control will automatically roll over if necessary
            /// </summary>
            Hundreds = 3,

            /// <summary>
            /// Increment the value by 1000, the control will automatically roll over if necessary
            /// </summary>
            Thousands = 4,

            /// <summary>
            /// Increment the value by 10000, the control will automatically roll over if necessary
            /// </summary>
            TensOfThousands = 5,

            /// <summary>
            /// Increment the value by 100000, the control will automatically roll over if necessary
            /// </summary>
            HundredsOfThousands = 6,

            /// <summary>
            /// Increment the value by 1000000, the control will automatically roll over if necessary
            /// </summary>
            Millions = 7,

            /// <summary>
            /// Increment the value by 10000000, the control will automatically roll over if necessary
            /// </summary>
            TensOfMillions = 8,

            /// <summary>
            /// Increment the value by 100000000, the control will automatically roll over if necessary
            /// </summary>
            HundredsOfMillions = 9,

            /// <summary>
            /// Increment the value by 10000000000, the control will automatically roll over 
            /// if necessary. Pleas note: We are based on an Int32 at the moment so the tenth
            /// digit can  only be 0,1 or 2
            /// </summary>
            Billions = 10,
        };

        #endregion

        #region construction

        /// <summary>
        /// Constructs an Odometer.
        /// </summary>
        public Odometer()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Odometer_Loaded);
            _timer.Duration = new Duration(TimeSpan.FromSeconds(1));
            _timer.Completed += new EventHandler(_timer_Tick);
        }

        #endregion

        #region InitialValue property

        /// <summary>
        /// Dependancy property for the InitialValue of the Odeometer
        /// </summary>
        public static readonly DependencyProperty InitialValueProperty =
            DependencyProperty.Register("InitialValue",
                                        typeof(int), typeof(Odometer),
                                        new PropertyMetadata(new PropertyChangedCallback(InitialValuePropertyChanged)));

        /// <summary>
        /// Initial value for the Odometer when execturing in the MeterMode
        /// InitialToFinal.
        /// </summary>
        public int InitialValue
        {
            get { return (int)GetValue(InitialValueProperty); }
            set
            {
                SetValue(InitialValueProperty, value);

            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void InitialValuePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Odometer instance = dependancy as Odometer;
            if (instance != null)
            {
                instance.SetInitialValue();
            }
        }

     
        #endregion

        #region FinalValue property

        /// <summary>
        /// Dependancy property for the FinalValue
        /// </summary>
        public static readonly DependencyProperty FinalValueProperty =
            DependencyProperty.Register("FinalValue",
                                        typeof(int), typeof(Odometer),
                                        null);

        /// <summary>
        /// Final value for the Odometer when execturing in the MeterMode
        /// InitialToFinal.
        /// </summary>
        public int FinalValue
        {
            get { return (int)GetValue(FinalValueProperty); }
            set
            {
                SetValue(FinalValueProperty, value);

            }
        }


    
        #endregion

        #region MeterMode property
        /// <summary>
        /// The dependancy property for the MeterModelColor property
        /// </summary>
        public static readonly DependencyProperty MeterModeProperty =
            DependencyProperty.Register("MeterMode",
                                        typeof(Mode), typeof(Odometer),
                                        null);

        /// <summary>
        /// Initial value for the Odometer
        /// </summary>
        public Mode MeterMode
        {
            get { return (Mode)GetValue(MeterModeProperty); }
            set
            {
                SetValue(MeterModeProperty, value);

            }
        }



        #endregion

        #region Digits property

        /// <summary>
        /// Dependancy property for our Digits property
        /// </summary>
        public static readonly DependencyProperty DigitsProperty =
            DependencyProperty.Register("Digits",
                                        typeof(double), typeof(Odometer),
                                        new PropertyMetadata(new PropertyChangedCallback(DigitsPropertyChanged)));

        /// <summary>
        /// number of digits to display
        /// </summary>
        public double Digits
        {
            get { return (double)GetValue(DigitsProperty); }
            set
            {
                SetValue(DigitsProperty, value);

            }
        }


        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void DigitsPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Odometer instance = dependancy as Odometer;
            if (instance != null)
            {
                instance.SetDigits();
            }
        }

        


        #endregion

        #region Interval property

        /// <summary>
        /// The dependancy property for the Interval property
        /// </summary>
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval",
                                        typeof(double), typeof(Odometer),
                                        new PropertyMetadata(new PropertyChangedCallback(IntervalPropertyChanged)));

        /// <summary>
        /// number of digits to display
        /// </summary>
        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set
            {
                SetValue(IntervalProperty, value);

            }
        }


        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void IntervalPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Odometer instance = dependancy as Odometer;
            if (instance != null)
            {
                instance._timer.Duration = new Duration(TimeSpan.FromSeconds(instance.Interval));
            }
        }




        #endregion

        #region Current Value (Calculated - Not a dependancy Property)

        private int CurrentValue
        {
            get
            {
                int res = 0;

                for (int i = 0; i < _digits.Count; i++)
                {
                    if (i > 0)
                    {
                        res = res * 10;
                    }
                    res += _digits[i].Digit;
                }

                return res;
            }
        }

        #endregion

        #region animation

        /// <summary>
        /// We are loaded lets start animating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Odometer_Loaded(object sender, RoutedEventArgs e)
        {
            if (MeterMode != Mode.Static)
            {
                _timer.Begin();
            }
        }

        /// <summary>
        /// The timer has ticked, lets Do the do.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Tick(object sender, EventArgs e)
        {
            switch (MeterMode)
            {
                case Mode.Static:
                    break;
                case Mode.AutoIncrement:
                    Increment();
                    break;
                case Mode.AutoDecrement:
                    Decrement();
                    break;
                case Mode.InitialToFinal:
                    MoveFromInitialToFinal();
                    break;
            }
            _timer.Begin();
        }


        /// <summary>
        /// Calculates an changes the value from initial to final in singa increments or decrements
        /// </summary>
        private void MoveFromInitialToFinal()
        {
            //note we deliberately omitt Initial == Final as this is a no-op
            if (InitialValue < FinalValue)
            {
                if (CurrentValue < FinalValue)
                    Increment();
            }
            else if (FinalValue < InitialValue)
            {
                if (CurrentValue > FinalValue)
                    Decrement();
            }
        }

        #endregion

        #region increment and decrement and set values
        /// <summary>
        /// Adds one to the value of the whole odometer, this is the equivilent to
        /// calling Increment(IncrementDigit.Ones);
        /// </summary>
        public void Increment()
        {
            Increment(IncrementDecrementDigit.Units);
        }

        /// <summary>
        /// When writing games and using the Odometer as a score control, you
        /// may wish to increment the score by a thousand rather than 1.This
        /// method allows you to pass in an IncrementDigit specifying which
        /// digit to rollover.
        /// </summary>
        /// <param name="digit">the digit to move</param>
        public void Increment(IncrementDecrementDigit digit)
        {
            int theDigit = ((int)digit);
            if (_digits.Count > theDigit - 1)
            {
                _digits[_digits.Count - theDigit ].Increment();
            }
        }

        /// <summary>
        /// Subtracts one from the value of the whole odometer
        /// </summary>
        public void Decrement()
        {
            Decrement(IncrementDecrementDigit.Units);
        }

        /// <summary>
        /// When writing games and using the Odometer as a score control, you
        /// may wish to decrement the score by a thousand rather than 1.This
        /// method allows you to pass in an IncrementDigit specifying which
        /// digit to rollover.
        /// </summary>
        /// <param name="digit">the digit to move</param>
        public void Decrement(IncrementDecrementDigit digit)
        {
            int theDigit = ((int)digit);
            if (_digits.Count > theDigit - 1)
            {
                _digits[_digits.Count - theDigit].Decrement();
            }
        }

 
        /// <summary>
        /// Set up the digits, we only do this if the digits are set before the
        /// value. If the value is set first we infer the number of digits from
        /// the value
        /// </summary>
        private void SetDigits()
        {
            if (_digits.Count == 0)
            {
                _stack.Children.Clear();
                OdometerDigit lastDigit = null;
                for (int i = 0; i < (int)Digits; i++)
                {
                    OdometerDigit digit = new OdometerDigit();
                    if (lastDigit != null)
                    {
                        digit.DecadePlus += new EventHandler<EventArgs>(lastDigit.LowerOrderDigitDecadePlus);
                        digit.DecadeMinus += new EventHandler<EventArgs>(lastDigit.LowerOrderDigitDecadeMinus);

                    }
                    lastDigit = digit;
                    _digits.Add(digit);
                    _stack.Children.Add(digit);
                }
            }
        }

        /// <summary>
        /// Puts the digits into their initial states, We expand the total number of
        /// digits if the amount present is not enough
        /// </summary>
        private void SetInitialValue()
        {
            double val = InitialValue;

            Double neededDigits = (Math.Log10(InitialValue) + 1) / Math.Log10(10);

            if (Digits < neededDigits)
            {
                _digits.Clear();
                Digits = neededDigits;
            }
            for (int i = _digits.Count; i > 0; i--)
            {
                double d = val % 10;
                OdometerDigit dg = _digits[i - 1];
                dg.SetInitialValue((int)d);
                val = val / 10;
            }

        }

        #endregion

    }
}
