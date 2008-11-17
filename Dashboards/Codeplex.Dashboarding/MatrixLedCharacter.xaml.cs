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
    /// <para>
    /// In marquee mode on request the character scrolls one led left, and raises
    /// the scroll out event (passing the coloumn that has left it). Down stream characters can 
    /// subscribe to this event and scroll themseleves. The data passed from the led before is the placed in column 5.
    /// This behavious causes a cascade scroll
    /// </para>
    /// <para>Note this control only scrolls content given to it. It is up to the controller to passint
    /// single columns that in total constitute a character in a language (indeed the
    /// columns cou;ld represent an extremely low resolution picture!
    /// </para>
    /// </summary>
    public partial class MatrixLedCharacter : UserControl
    {
        /// <summary>
        /// stores a list of columns (5 of them) which are in turn lists of row states
        /// </summary>
        private List<List<bool>> columns = new List<List<bool>>(5);

        /// <summary>
        /// This cell is scrolling out a column of data
        /// </summary>
        public event EventHandler<MatrixScrollEventArgs> ScrollOut;

        /// <summary>
        /// Constructs and empty character
        /// </summary>
        public MatrixLedCharacter()
        {
            InitializeComponent();
            Clear();

            LedOffColor = Color.FromArgb(0x22, 0xdd, 0x00, 0x00);
            LedOnColor = Color.FromArgb(0xFF, 0xdd, 0x00, 0x00);

            
        }


        #region Text property
        /// <summary>
        /// The dependancy property for the Text
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MatrixLedCharacter), new PropertyMetadata(new PropertyChangedCallback(TextPropertyChanged)));

        /// <summary>
        /// Gets or sets the text (single character) of the control.Setting the text initializes
        /// all leds.
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
            MatrixLedCharacter instance = dependancy as MatrixLedCharacter;


            if (instance != null)
            {
                if (instance.Text != null)
                {
                    instance.SetLedsFromCharacter();
                }
            }
        }

    
      


        #endregion

        #region LedOnColor property
        /// <summary>
        /// The dependancy property for the LedOn colr
        /// </summary>
        public static readonly DependencyProperty LedOnColorProperty =
            DependencyProperty.Register("LedOnColor", typeof(Color), typeof(MatrixLedCharacter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

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

        /// <summary>
        /// Our dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void ColorPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            MatrixLedCharacter instance = dependancy as MatrixLedCharacter;


            if (instance != null)
            {
                if (instance.LedOnColor != null)
                {
                    instance.SetLedsFromState();
                }
            }
        }


        #endregion

        #region LedOffColor property

        /// <summary>
        /// THe dependancy property for the LedOffColor
        /// </summary>
        public static readonly DependencyProperty LedOffColorProperty =
            DependencyProperty.Register("LedOffColor", typeof(Color), typeof(MatrixLedCharacter), new PropertyMetadata(new PropertyChangedCallback(ColorPropertyChanged)));

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

        #endregion

        
        /// <summary>
        /// Initialize all columns from a definition in the Character defintion
        /// </summary>
        private void SetLedsFromCharacter()
        {
           byte [] bytes = MatrixLedCharacterDefintions.GetDefintion(Text);
           columns.Clear();
           for (int i = 0; i < bytes.Length - 1; i++)
           {
               List<bool> n = new List<bool> 
                {
                    (bytes[i] & 0x40) != 0,
                    (bytes[i] & 0x20) != 0,
                    (bytes[i] & 0x10) != 0,
                    (bytes[i] & 0x08) != 0,
                    (bytes[i] & 0x04) != 0,
                    (bytes[i] & 0x02) != 0,
                    (bytes[i] & 0x01) != 0,
                };
               columns.Add(n);
           }
           SetLedsFromState();
        }



        /// <summary>
        /// Forces this cell to scroll one column and passes the next column. This will cascade to any connected
        /// cells. Usually on a marquee style control this means only the right most
        /// character gets ticked manually the rest do through chained events
        /// </summary>
        public void ScrollOne(object sender, MatrixScrollEventArgs args)
        {
            List<bool> toPassOver = columns[0];
            columns.RemoveAt(0);
            columns.Add(args.Column);
            if (ScrollOut != null)
            {
                ScrollOut(this, new MatrixScrollEventArgs { Column = toPassOver });
            }
            SetLedsFromState();
        }

        /// <summary>
        /// Set the leds on or off acording to the buffer state
        /// </summary>
        private void SetLedsFromState()
        {
            for (int x = 0; x < columns.Count; x++)
            {
                ProcessColumn(x);
            }
        }

        /// <summary>
        /// Switches on or off leds fr a single column
        /// </summary>
        /// <param name="x">the column number</param>
        private void ProcessColumn(int x)
        {
            for (int y = 0; y < columns[x].Count; y++)
            {
                bool on = columns[x][y];
                Ellipse el = LayoutRoot.FindName(String.Format("_l{0}_{1}", x, y)) as Ellipse;
                TureLedOnOrOff(on, el);

            }
        }

        /// <summary>
        /// Sets the led color for a single led
        /// </summary>
        /// <param name="on">true if the led is on false otherwise</param>
        /// <param name="el">The ellipse repesesenting the led</param>
        private  void TureLedOnOrOff(bool on, Ellipse el)
        {
            if (el != null)
            {
                el.Fill = new SolidColorBrush((on) ? LedOnColor: LedOffColor);
            }
        }



        /// <summary>
        /// Clear the control down to all leds are off
        /// </summary>
        internal void Clear()
        {
            columns.Clear();
            for (int i = 0; i < 5; i++)
            {
                columns.Add(new List<bool> { false, false, false, false, false, false, false });
            }
            SetLedsFromState();
        }
    }

    /// <summary>
    /// Event args describing the scroll of a matrix
    /// </summary>
    public class MatrixScrollEventArgs: EventArgs
    {
        /// <summary>
        /// New inbound column of data
        /// </summary>
        public List<bool> Column { get; set; }
    }

}
