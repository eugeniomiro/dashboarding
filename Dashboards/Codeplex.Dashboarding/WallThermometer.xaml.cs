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
            SetValue(MercuryColorRangeProperty, new ColorPointCollection());
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



        /// <summary>
        /// For animation we set the value of the contained thermometer, then
        /// do our stuff
        /// </summary>
        protected override void Animate()
        {
            _delegate.Value = Value;
        }
    }
}
