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
using System.Windows.Threading;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MatrixLedMarquee : UserControl
    {
        #region MarqueeMode enum

        /// <summary>
        /// Specifies the render mode of the MatrixLedMarquee
        /// </summary>
        public enum MarqueeMode
        {
            /// <summary>
            /// renders the current text and raises the MarqueeFinished event to
            /// allow you to set new text
            /// </summary>
            SingleShot,

            /// <summary>
            /// Text is wrapped.When the last letter is 
            /// rendered, we start agin with the first
            /// </summary>
            Continious,

            /// <summary>
            /// Just display the text, don't scroll (see TextAlign) 
            /// </summary>
            Motionless
        }

        #endregion

        #region private members

        /// <summary>
        /// The timer that controls the scroll rate
        /// </summary>
        private DispatcherTimer _timer = new DispatcherTimer();

        private List<MatrixLedCharacter> _characters = new List<MatrixLedCharacter>();

        #endregion

        #region construction
        /// <summary>
        /// A scrolling marquee of led characters. Just like any self respecting dry cleaners had in
        /// the window in 1989.
        /// </summary>
        public MatrixLedMarquee()
        {
            InitializeComponent();
            _timer.Tick += new EventHandler(timer_Completed);

            TimerDuration = new Duration(new TimeSpan(0, 0, 1));
            LedOffColor = Color.FromArgb(0x22, 0xdd, 0x00, 0x00);
            LedOnColor = Color.FromArgb(0xFF, 0xdd, 0x00, 0x00);

            Loaded += new RoutedEventHandler(MatrixLedMarquee_Loaded);
        }
        #endregion

        #region MarqueeFinished event

        /// <summary>
        /// Event raised when the last character of a marquee is completely
        /// on the surface of the display. It is an indicator to tell you that
        /// you can set new text.
        /// </summary>
        public event EventHandler<EventArgs> MarqueeFinished;

        /// <summary>
        /// raises the MarqueeFinishedEvent if any one is listening
        /// </summary>
        private void OnMarqueeFinished()
        {
            if (MarqueeFinished != null)
            {
                MarqueeFinished(this, EventArgs.Empty);
            }
        }

        #endregion

        #region LedOnColor property
        /// <summary>
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty LedOnColorProperty =
            DependencyProperty.Register("LedOnColor", typeof(Color), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// The on color of all leds in the marquee
        /// </summary>
        public Color LedOnColor
        {
            get { return (Color)GetValue(LedOnColorProperty); }
            set
            {
                SetValue(LedOnColorProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;


            if (instance != null)
            {
                if (instance.LedOnColor != null)
                {
                    instance.SetLedsFromState();
                }
            }
        }

        private void SetLedsFromState()
        {
            foreach (MatrixLedCharacter ch in _characters)
            {
                ch.LedOffColor = LedOffColor;
                ch.LedOnColor = LedOnColor;
            }
        }


        #endregion

        #region LedOffColor property

        /// <summary>
        /// THe dependancy property for the LedOffColor
        /// </summary>
        public static readonly DependencyProperty LedOffColorProperty =
            DependencyProperty.Register("LedOffColor", typeof(Color), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

        /// <summary>
        /// the off color of leds in the marquee
        /// </summary>
        public Color LedOffColor
        {
            get { return (Color)GetValue(LedOffColorProperty); }
            set
            {
                SetValue(LedOffColorProperty, value);
            }
        }

        #endregion

        #region TimerDuration property
        /// <summary>
        /// The dependancy property for the Panels properties
        /// </summary>
        public static readonly DependencyProperty TimerDurationProperty =
            DependencyProperty.Register("TimerDuration", typeof(Duration), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(TimerDurationPropertyChanged)));

        /// <summary>
        /// Timer duration between scroll steps
        /// </summary>
        public Duration TimerDuration
        {
            get { return (Duration)GetValue(TimerDurationProperty); }
            set
            {
                SetValue(TimerDurationProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, update the instances timer duration
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TimerDurationPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;
            if (instance != null)
            {

                instance._timer.Interval = instance.TimerDuration.TimeSpan;
            }
        }




        #endregion

        #region Panels property
        /// <summary>
        /// The dependancy property for the Panels properties
        /// </summary>
        public static readonly DependencyProperty PanelsProperty =
            DependencyProperty.Register("Panels", typeof(int), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(PanelsPropertyChanged)));

        /// <summary>
        /// Hi colour in the blend
        /// </summary>
        public int Panels
        {
            get { return (int)GetValue(PanelsProperty); }
            set
            {
                SetValue(PanelsProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void PanelsPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;


            if (instance != null)
            {

                instance.InitializePanels();
            }
        }

        /// <summary>
        /// Fill the control with the required number of panels
        /// </summary>
        private void InitializePanels()
        {

            _stackpanel.Children.Clear();
            _characters.Clear();
            for (int i = 0; i < Panels; i++)
            {
                MatrixLedCharacter ch = new MatrixLedCharacter();
                ch.LedOnColor = LedOnColor;
                ch.LedOffColor = LedOffColor;
                _characters.Add(ch);
                _stackpanel.Children.Add(ch);

                if (_characters.Count > 1)
                {
                    MatrixLedCharacter prev = _characters[i - 1];
                    ch.ScrollOut += new EventHandler<MatrixScrollEventArgs>(prev.ScrollOne);
                }
            }

        }


        #endregion

        #region Text property
        /// <summary>
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(TextPropertyChanged)));

        /// <summary>
        /// Set the text for the marquee. If we are animating we stop, clear the marquee and initialize. Note
        /// this is pretty bruit force but nice users only set the Text when
        /// <list type="bullet">
        /// <item>They are initializing the control from XAML</item>
        /// <item>They have received an event indicating the last message has rendered.</item>
        /// </list>
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;


            if (instance != null)
            {
                instance.TextChanged();
            }
        }




        #endregion

        #region TextAlignment property
        /// <summary>
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty TextAlignProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(TextAlignPropertyChanged)));

        /// <summary>
        /// Set the text alignment for the marquee. 
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignProperty); }
            set
            {
                SetValue(TextAlignProperty, value);
            }
        }

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextAlignPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;


            if (instance != null)
            {
                if (instance.Text != null)
                {
                    instance.TextChanged();
                }
            }
        }




        #endregion


        #region MarqueeMode property
        /// <summary>
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty MarqueeModeProperty =
            DependencyProperty.Register("Mode", typeof(MarqueeMode), typeof(MatrixLedMarquee), new PropertyMetadata(new PropertyChangedCallback(ModePropertyChanged)));

        /// <summary>
        /// Sets the MarqueeMode of the MatrixLedMarquee
        /// </summary>
        public MarqueeMode Mode
        {
            get { return (MarqueeMode)GetValue(MarqueeModeProperty); }
            set
            {
                SetValue(MarqueeModeProperty, value);
            }
        }


        private static void ModePropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedMarquee instance = dependancy as MatrixLedMarquee;


            if (instance != null && instance.Text != null)
            {
                instance.TextChanged();
            }
        }


        #endregion



        /// <summary>
        /// We are loaded start the timer and let the scrolling begin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MatrixLedMarquee_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }


        /// <summary>
        /// Timer has ticked
        /// </summary>
        void timer_Completed(object sender, EventArgs e)
        {
            if (_characters.Count > 0)
            {
                Animate();
            }
        }

        /// <summary>
        /// The text has changed. for continious and motionless operation
        /// we clear the screen. For one shot we leave it as the user will
        /// want text to join on to the previous
        /// </summary>
        private void TextChanged()
        {
            if (Mode != MarqueeMode.SingleShot)
            {
                foreach (MatrixLedCharacter ch in _characters)
                {
                    ch.Clear();
                }
            }
            if (Mode == MarqueeMode.Motionless)
            {
                InitializeMotionlessText();
            }
            else
            {
                InitializeAnimatedText();
            }
        }

        #region text alignment

        /// <summary>
        /// Initializes the text justified in the entire marquee.
        /// </summary>
        private void InitializeMotionlessText()
        {
            string formatted = "".PadRight(_characters.Count());
            if (TextAlignment == TextAlignment.Left)
            {
                formatted = AlignTextLeft();
            }
            else if (TextAlignment == TextAlignment.Right)
            {
                formatted = AlignTextRight();
            }
            else if (TextAlignment == TextAlignment.Center)
            {
                formatted = AlignTextCenter();
            }
            SetAlignedText(formatted);


        }

        private string AlignTextCenter()
        {
            //Make sure we are over sized
            string start = "".PadLeft(_characters.Count) + Text + "".PadRight(_characters.Count);

            return start.Substring((start.Length - _characters.Count) / 2, _characters.Count);
        }

        /// <summary>
        /// Align our Text property right in a string of _characters.Count chars
        /// we throw away any extras
        /// </summary>
        /// <returns>A justified string</returns>
        private string AlignTextRight()
        {
            string formatted = "";
            if (Text.Length > _characters.Count)
            {
                formatted = Text.Substring(Text.Length - _characters.Count);
            }
            else
            {
                formatted = Text.PadLeft(_characters.Count);
            }
            return formatted;
        }

        /// <summary>
        /// Align our Text property Left in a string of _characters.Count chars
        /// </summary>
        /// <returns>A justified string</returns>
        private string AlignTextLeft()
        {
            string formatted = "";
            if (Text.Length > _characters.Count)
            {
                formatted = Text.Substring(0, _characters.Count);
            }
            else
            {
                formatted = Text.PadRight(_characters.Count);
            }
            return formatted;
        }

        /// <summary>
        /// Sets the static text of the marquee to a fixed value
        /// </summary>
        /// <param name="formatted"></param>
        private void SetAlignedText(string formatted)
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                if (i < formatted.Length)
                {
                    _characters[i].Text = "" + formatted[i];
                }
            }
        }

        #endregion

        /// <summary>
        /// Initialize a text string that is to be animated
        /// </summary>
        private void InitializeAnimatedText()
        {
            _offset = 0;
            _textOffset = 0;
            if (Text.Length > 0)
            {
                _ledStates = MatrixLedCharacterDefintions.GetDefintion("" + Text[_textOffset]);
                _textExists = true;
            }
            else
            {
                _textExists = false;
            }
        }

        /// <summary>
        /// Do we have text to animate
        /// </summary>
        private bool _textExists;

        /// <summary>
        /// Current led to render
        /// </summary>
        private byte[] _ledStates = null;

        /// <summary>
        /// current column in the current led 
        /// </summary>
        private int _offset = 0;

        /// <summary>
        /// Current offset within the text
        /// </summary>
        private int _textOffset = 0;

        /// <summary>
        /// Animate the marquee, if we need a new character from the string we get one
        /// otherwise we access the next vertical strip of leds from the current. To keep performance uniform
        /// a control with no text animates off leds. This is a noop for motionless output
        /// </summary>
        private void Animate()
        {
            if (Mode != MarqueeMode.Motionless)
            {
                AnimanteNonMotionless();
            }
        }

        /// <summary>
        /// We know we are not motionless so we animate
        /// </summary>
        private void AnimanteNonMotionless()
        {
            MatrixLedCharacter first = _characters[_characters.Count - 1];
            if (!_textExists)
            {
                first.ScrollOne(null, new MatrixScrollEventArgs { Column = new List<bool> { false, false, false, false, false, false } });
            }
            else
            {
                first.ScrollOne(null, GetNextVerticalStrip());
            }

        }

        /// <summary>
        /// Gets the next vertical strip of LEDS and scrolls the current point on. If a new 
        /// character is required it is brought into play
        /// </summary>
        /// <returns></returns>
        private MatrixScrollEventArgs GetNextVerticalStrip()
        {
            byte b = _ledStates[_offset];
            MatrixScrollEventArgs args = new MatrixScrollEventArgs
            {
                Column = new List<bool> 
                {
                    (b & 0x40) != 0,
                    (b & 0x20) != 0,
                    (b & 0x10) != 0,
                    (b & 0x08) != 0,
                    (b & 0x04) != 0,
                    (b & 0x02) != 0,
                    (b & 0x01) != 0,

                }
            };
            RackCharacter();
            return args;
        }

        /// <summary>
        /// Goto the next character in the string. Usually not so tricky except when we run 
        /// out of gas. Then we act accoring to the mode of the control
        /// <list type="bullet">
        /// <item>Continous - we start the string over again</item>
        /// <item>SingleShot - scroll it once then stop. We raise an event 
        /// to tell the user that we are complete</item>
        /// </list>
        /// </summary>
        private void RackCharacter()
        {
            _offset += 1;
            if (_offset >= _ledStates.Length)
            {
                _offset = 0;
                _textOffset += 1;
                if (_textOffset < Text.Length)
                {
                    _ledStates = MatrixLedCharacterDefintions.GetDefintion("" + Text[_textOffset]);
                }
                else
                {
                    if (Mode == MarqueeMode.Continious)
                    {
                        _textOffset = 0;
                        _ledStates = MatrixLedCharacterDefintions.GetDefintion("" + Text[_textOffset]);
                    }
                    else if (Mode == MarqueeMode.SingleShot)
                    {
                        _textExists = false;
                        OnMarqueeFinished();
                    }
                    // if continuous scroll reset _textoffset to 0
                    // else raise text complete event

                }
            }
        }
    }
}
