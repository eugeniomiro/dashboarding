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
    /// A wall thermometer encapsulates a plain thermometer and decorates it visually with a backing plate and
    /// text for the temperature in decades.
    /// <para>Programatically it also decarates the PlainThermometer class
    /// passing through properties and adding its own.</para>
    /// </summary>
    public partial class WallThermometer : Dashboard
    {
        /// <summary>
        /// Constructs a WallThermometer
        /// </summary>
        public WallThermometer() :base()
        {
            InitializeComponent();
            _delegate.TextColor = Colors.Black;
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OneOfMyPropertiesChanged);
            _delegate.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OneOfTheDelegatesPropertiesChanged);
            SetValue(MercuryColorRangeProperty, new ColorPointCollection());
        }

        /// <summary>
        /// One of the delegates properties changed if it is value then raise the onproperty changed event for 
        /// us by setting our value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OneOfTheDelegatesPropertiesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Minimum":
                    if (_delegate.Minimum != Minimum)
                        Minimum = _delegate.Minimum;
                    break;
                case "Maximum":
                    if (_delegate.Maximum != Maximum)
                        Maximum = _delegate.Maximum;
                    break;
                case "Value":
                    if (_delegate.Value != Value)
                        Value = _delegate.Value;
                    break;
            }
        }

        /// <summary>
        /// One of my propertis changed, if this is a property we need to pass to our delegate
        /// we do so
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        void OneOfMyPropertiesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Minimum":
                    if (_delegate.Minimum != Minimum)
                        _delegate.Minimum = Minimum;
                    break;
                case "Maximum":
                    if (_delegate.Maximum != Maximum)
                        _delegate.Maximum = Maximum;
                    break;
                case "Value":
                    if (_delegate.Value != Value)
                        _delegate.Value = Value;
                    break;
            }
        }


        #region MercuryColorRange property

        /// <summary>
        /// Dependancy property for out MercuryColor property
        /// </summary>
        public static readonly DependencyProperty MercuryColorRangeProperty =
            DependencyProperty.Register("MercuryColorRange", typeof(ColorPointCollection), typeof(WallThermometer), new PropertyMetadata(new PropertyChangedCallback(MercuryColorRangeChanged)));

        /// <summary>
        /// The point in the range (0..100) where this color takes effect
        /// </summary>
        public ColorPointCollection MercuryColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(MercuryColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(MercuryColorRangeProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void MercuryColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            WallThermometer instance = dependancy as WallThermometer;
            if (instance != null)
            {
                instance._delegate.MercuryColorRange = instance.MercuryColorRange;
            }
        }


        #endregion


        #region TextVisibility property
        /// <summary>
        /// The dependancy property for theTextVisibilityiColor property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(WallThermometer), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        /// <summary>
        /// Color of the text that shows the percentage
        /// </summary>
        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set
            {
                SetValue(TextVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            WallThermometer instance = dependancy as WallThermometer;


            if (instance != null)
            {
                instance.SetTextVisibility();
            }
        }

        /// <summary>
        /// Set the visibiliity of the thermometer text and resize the wood to suite
        /// </summary>
        private void SetTextVisibility()
        {
            _delegate.TextVisibility = TextVisibility;
            _wood.Height = (TextVisibility == Visibility.Visible) ? 142 : 128;
        }

        #endregion



        #region IsBidirectional property

        /// <summary>
        /// Identifies the IsBidirectional attached property
        /// </summary>
        public static readonly DependencyProperty IsBidirectionalProperty =
            DependencyProperty.Register("IsBidirectional", typeof(Boolean), typeof(WallThermometer), new PropertyMetadata(new PropertyChangedCallback(IsBidirectionalPropertyChanged)));

        /// <summary>
        /// Gets or sets a value to determin if this dashboard is bidrectional. IsBiderectional == false means that
        /// the control shows values. IsBidirectional == true means that the user can interact with the control
        /// to ser values. We implement this here and not derive from BiDirectional Dashboard control because
        /// the plain thermometer does the heavy lifting we just delgate to it
        /// </summary>
        public Boolean IsBidirectional
        {
            get
            {
                Boolean res = (Boolean)GetValue(IsBidirectionalProperty);
                return res;
            }
            set
            {
                SetValue(IsBidirectionalProperty, value);
                _delegate.IsBidirectional = value;
            }
        }



        /// <summary>
        /// The value of the IsBidirectional property has changed. We call animate to allow any focus
        /// handle to be rendered
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void IsBidirectionalPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDashboard instance = dependancy as BidirectionalDashboard;
            if (instance != null)
            {
                instance.IsBidirectional = (bool)args.NewValue;
            }
        }


        #endregion

 

        /// <summary>
        /// For animation we set the value of the contained thermometer, then
        /// do our stuff
        /// </summary>
        protected override void Animate()
        {
            _delegate.Value = Value;
        }

        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected override Grid ResourceRoot
        {
            get { return LayoutRoot; }
        }


    }
}
