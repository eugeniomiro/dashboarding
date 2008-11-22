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
    /// A SixteenSegmentLED is a single character represented using LEDS. As the name suggests
    /// the control is comprised of sixteen seperate LEDS which leads to a more detailed representation
    /// than a Seven, eight or fourteen LED display.
    /// </summary>
    public partial class SixteenSegmentLED : UserControl
    {
        /// <summary>
        /// Names for each component bit
        /// </summary>
        internal enum Leds { THL, THR, TVL, TVM, TVR, MHL, MHR, BVL, BVM, BVR, BHL, BHR, TDL, TDR, BDL, BDR };

        /// <summary>
        /// Stores a map to paths from segment names
        /// </summary>
        private Dictionary<Leds, Path> _leds = new Dictionary<Leds, Path>();

        #region Character Leds mappings

        /// <summary>
        /// Describes the leds to turn on for each character
        /// </summary>
        private static Dictionary<string, List<Leds>> _characterLeds = new Dictionary<string, List<Leds>>
        {
            { " ", new List<Leds>() }, 
            { "A", new List<Leds>{Leds.THL, Leds.THR, Leds.TVL, Leds.TVR, Leds.BVL, Leds.BVR, Leds.MHL, Leds.MHR} }, 
            { "B", new List<Leds>{Leds.THL, Leds.THR, Leds.TVM, Leds.BVM, Leds.MHR, Leds.TVR, Leds.BVR, Leds.BHL, Leds.BHR  }  }, 
            { "C", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL }  }, 
            { "D", new List<Leds>{Leds.THL, Leds.THR, Leds.TVM, Leds.BVM, Leds.TVR, Leds.BVR, Leds.BHL, Leds.BHR  }  }, 
           
            { "E", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL,  Leds.MHR, Leds.MHL}  },           
            { "F", new List<Leds>{Leds.THL, Leds.THR,  Leds.TVL, Leds.BVL,  Leds.MHL}  },           
            { "G", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL,  Leds.MHR, Leds.BVR}  },           
            { "H", new List<Leds>{Leds.TVL, Leds.TVR, Leds.BVL, Leds.BVR, Leds.MHL, Leds.MHR} }, 
            { "I", new List<Leds>{Leds.TVM, Leds.BVM} }, 
            { "J", new List<Leds>{Leds.BHL, Leds.BHR , Leds.BVL , Leds.BVR, Leds.TVR}  }, 

             { "K", new List<Leds>{Leds.TVL, Leds.BVL, Leds.MHL, Leds.TDR, Leds.BDR} }, 

             { "L", new List<Leds>{ Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL }  }, 


          { "M", new List<Leds>{ Leds.TVL, Leds.BVL, Leds.TVR, Leds.BVR, Leds.TDL, Leds.TDR }  }, 
          { "N", new List<Leds>{ Leds.TVL, Leds.BVL, Leds.TVR, Leds.BVR, Leds.TDL, Leds.BDR }  }, 


            { "O", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL , Leds.BVR,Leds.TVR}  }, 
            { "P", new List<Leds>{Leds.THL, Leds.THR,  Leds.TVL, Leds.BVL,  Leds.MHL,Leds.MHR,Leds.TVR}  },           

            { "Q", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL , Leds.BVR,Leds.TVR, Leds.BDR}  }, 
            
            
            { "U", new List<Leds>{Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL , Leds.BVR,Leds.TVR}  }, 
            { "R", new List<Leds>{Leds.THL, Leds.THR,  Leds.TVL, Leds.BVL,  Leds.MHL,Leds.MHR,Leds.TVR, Leds.BDR}  },           
           { "S", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVR,  Leds.MHR, Leds.MHL}  },           
            { "T", new List<Leds>{Leds.TVM, Leds.BVM, Leds.THL, Leds.THR} }, 

            { "V", new List<Leds>{Leds.TVL, Leds.BVL, Leds.BDL, Leds.TDR} }, 
            { "W", new List<Leds>{Leds.TVL, Leds.BVL, Leds.BDL, Leds.BDR, Leds.TVR, Leds.BVR} }, 

            { "X", new List<Leds>{Leds.TDL, Leds.TDR, Leds.BDL, Leds.BDR} }, 
            
            { "Y", new List<Leds>{Leds.TDL, Leds.TDR, Leds.BVM} }, 
            { "Z", new List<Leds>{Leds.TDR, Leds.BDL, Leds.THL, Leds.THR, Leds.BHL, Leds.BHR} }, 


            { "0", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL , Leds.BVR,Leds.TVR}  }, 
            { "1", new List<Leds>{Leds.TVR, Leds.BVR} }, 
            { "2", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.BVL,  Leds.MHR, Leds.MHL, Leds.TVR}  },           
            { "3", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.BVR,  Leds.MHR, Leds.MHL, Leds.TVR, Leds.MHR}  },           

            { "4", new List<Leds>{ Leds.TVL, Leds.MHR, Leds.MHL, Leds.TVR, Leds.BVR}  },           


            { "5", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BDR , Leds.BHR,   Leds.MHL, Leds.TVL}  },           

            { "6", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL,  Leds.MHR, Leds.MHL,  Leds.BVR}  },           

            { "7", new List<Leds>{Leds.THL, Leds.THR, Leds.TVR, Leds.BVR}  },           



            { "8", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL, Leds.BVL,  Leds.MHR, Leds.MHL, Leds.TVR, Leds.BVR}  },           
            { "9", new List<Leds>{Leds.THL, Leds.THR, Leds.BHL, Leds.BHR , Leds.TVL,  Leds.MHR, Leds.MHL, Leds.TVR, Leds.BVR}  },           

        };



        

        #endregion

        /// <summary>
        /// Constructs a SSLED
        /// </summary>
        public SixteenSegmentLED()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(SixteenSegmentLED_Loaded);


            LedOffColor = Color.FromArgb(0x50, 0x5e, 0x57, 0x57);
            LedOnColor = Color.FromArgb(0xFF, 0x00, 0x99, 0x00);

        }

        /// <summary>
        /// We are loaded display out character
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        void SixteenSegmentLED_Loaded(object sender, RoutedEventArgs e)
        {
            StoreLedInformation();
            Animate();
        }

        
        #region DisplayCharacter property

        /// <summary>
        /// The dependancy property for the DisplayCharacter property
        /// </summary>
        public static readonly DependencyProperty DisplayCharacterProperty =
            DependencyProperty.Register("DisplayCharacter",
                                        typeof(string), typeof(SixteenSegmentLED),
                                        new PropertyMetadata(new PropertyChangedCallback(DisplayCharacterPropertyChanged)));

        /// <summary>
        /// Current Display char 0..9 A..Z
        /// </summary>
        public string DisplayCharacter
        {
            get { return (string)GetValue(DisplayCharacterProperty); }
            set
            {
                SetValue(DisplayCharacterProperty, value);

            }
        }

        /// <summary>
        /// Property changed, show the new value
        /// </summary>
        /// <param name="dependancy"></param>
        /// <param name="args"></param>
        private static void DisplayCharacterPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            SixteenSegmentLED instance = dependancy as SixteenSegmentLED;
            if (instance != null)
            {
                instance.Animate();
            }
        }


        #endregion

        #region LedOnColor property

        /// <summary>
        /// The dependancy property for the LedOnColor property
        /// </summary>
        public static readonly DependencyProperty LedOnColorProperty =
            DependencyProperty.Register("LedOnColor", typeof(Color), typeof(SixteenSegmentLED), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public Color LedOnColor
        {
            get { return (Color)GetValue(LedOnColorProperty); }
            set
            {
                SetValue(LedOnColorProperty, value);
            }
        }

        #endregion

        #region LedOffColor property

        /// <summary>
        /// The dependancy property for the LedOffColor property
        /// </summary>
        public static readonly DependencyProperty LedOffColorProperty =
            DependencyProperty.Register("LedOffColor", typeof(Color), typeof(SixteenSegmentLED), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public Color LedOffColor
        {
            get { return (Color)GetValue(LedOffColorProperty); }
            set
            {
                SetValue(LedOffColorProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            SixteenSegmentLED instance = dependancy as SixteenSegmentLED;


            if (instance != null)
            {
                instance.Animate();
            }
        }

        #endregion




        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        private void Animate()
        {
            SetAllLedsOff();
            SetRequiresLedsON();
        }

        private void SetAllLedsOff()
        {
            foreach (Path path in _leds.Values)
            {
                path.Fill = new SolidColorBrush(LedOffColor);
            }
        }

        private void SetRequiresLedsON()
        {
            if (_leds.Count == 0 || String.IsNullOrEmpty(DisplayCharacter) || DisplayCharacter == " ")
                return;
            if (DisplayCharacter.Length > 1)
                ShowError();

           if (_characterLeds.ContainsKey(DisplayCharacter.ToUpper()))
           {
               var leds = _characterLeds[DisplayCharacter.ToUpper()];
               foreach (Leds led in leds)
               {
                   _leds[led].Fill = new SolidColorBrush(LedOnColor);
               }
           }

        }

        /// <summary>
        /// We show an error by turning everything on!
        /// </summary>
        private void ShowError()
        {
            foreach (Path path in _leds.Values)
            {
                path.Fill = new SolidColorBrush(LedOnColor);
            }
        }


        private void StoreLedInformation()
        {
            _leds.Add(Leds.BDL, _bdl);
            _leds.Add(Leds.BDR, _bdr);
            _leds.Add(Leds.BHL, _bhl);
            _leds.Add(Leds.BHR, _bhr);
            _leds.Add(Leds.BVL, _bvl);
            _leds.Add(Leds.BVM, _bvm);
            _leds.Add(Leds.BVR, _bvr);
            _leds.Add(Leds.MHL, _mhl);
            _leds.Add(Leds.MHR, _mhr);
            _leds.Add(Leds.TDL, _tdl);
            _leds.Add(Leds.TDR, _tdr);
            _leds.Add(Leds.THL, _thl);
            _leds.Add(Leds.THR, _thr);
            _leds.Add(Leds.TVL, _tvl);
            _leds.Add(Leds.TVM, _tvm);
            _leds.Add(Leds.TVR, _tvr);
        }


    }
}
