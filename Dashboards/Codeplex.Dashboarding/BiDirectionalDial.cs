using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics.CodeAnalysis;
namespace Codeplex.Dashboarding
{
    /// <summary>
    /// Base class for all dials. We have the base properties common to all 
    /// of the controls (Needle color range etc)
    /// </summary>
    public abstract partial class BidirectionalDial : BidirectionalDashboard
    {

        #region FaceColorRange property

        /// <summary>
        /// Dependancy property for the FaceColor attached property
        /// </summary>

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));

        /// <summary>
        /// Specifies the face color at points in the range. A single color point with
        /// a value of 0 specifies the color for all
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public ColorPointCollection FaceColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(FaceColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(FaceColorRangeProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our dependany property has changed, update the face color
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        private static void FaceColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {

                instance.SetFaceColor();
                instance.OnPropertyChanged("FaceColorRange");
            }
        }



        #endregion

        #region NeedleColorRange property

        /// <summary>
        /// The  Dependancy property for the NeedleColor attached property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));

        /// <summary>
        /// Specifies what color the needle is a various point is the range
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public ColorPointCollection NeedleColorRange
        {
            get
            {
                ColorPointCollection res = (ColorPointCollection)GetValue(NeedleColorRangeProperty);
                return res;
            }
            set
            {
                SetValue(NeedleColorRangeProperty, value);
                Animate();
            }
        }

        /// <summary>
        /// Our needle color has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void NeedleColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {

                instance.SetNeedleColor();
                instance.OnPropertyChanged("NeedleColorRange");
            }
        }

      

        #endregion

        #region TextColor

        /// <summary>
        /// The Dependancy property for the TextColor attached property
        /// </summary>

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(TextColorChanged)));

        /// <summary>
        /// The color of the text used to show the percentage
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
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
                Animate();
            }
        }

        /// <summary>
        /// Our TextColor dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        private static void TextColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {
                instance.SetTextColor();


                instance.OnPropertyChanged("TextColor");
            }
        }

       




        #endregion

        #region TextVisibility property
        /// <summary>
        /// The dependancy property for theTextVisibility attached property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));


        /// <summary>
        /// Show or hide the text according to the visibility
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
        /// Our TextVivibility dependany property has changed, deal with it
        /// </summary>
        /// <param name="dependancy">the dependancy object</param>
        /// <param name="args">arguments</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;


            if (instance != null)
            {

                instance.SetTextVisibility();


                instance.OnPropertyChanged("TextVisibility");

            }
        }

      



        #endregion


        #region Bidirection functionality

        /// <summary>
        /// Highlight the grab handle as the mouse is in
        /// </summary>
        protected override void ShowGrabHandle()
        {
            base.ShowGrabHandle();
            GrabHighlight.Fill = new SolidColorBrush(Color.FromArgb(0x4c, 0xde, 0xf0, 0xf6));
        }

        /// <summary>
        /// Stop the highlight of the grab handle the mouse is out
        /// </summary>
        protected override void HideGrabHandle()
        {
            base.HideGrabHandle();
            GrabHighlight.Fill = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Mouse is moving, move the diagram
        /// </summary>
        /// <param name="mouseDownPosition">origin of the drag</param>
        /// <param name="currentPosition">where the mouse is now</param>
        protected override void OnMouseGrabHandleMove(Point mouseDownPosition, Point currentPosition)
        {
            base.OnMouseGrabHandleMove(mouseDownPosition, currentPosition);
            double cv = CalculateRotationAngle(currentPosition);
            SetCurrentNormalizedValue(cv);
            Animate();
        }



        #endregion


        /// <summary>
        /// Update your face color from the property value
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        protected abstract void SetFaceColor();

        /// <summary>
        /// Update your needle color from the property value
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        protected abstract void SetNeedleColor();

        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        protected abstract void SetTextColor();

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected abstract void SetTextVisibility();

        /// <summary>
        /// gets the shape used to highlight the fact that the mouse is in 
        /// drag control
        /// </summary>
        protected abstract Shape GrabHighlight { get; }

        /// <summary>
        /// Based on the rotation angle, set the normalized current value
        /// </summary>
        /// <param name="cv">rotation angle</param>
        protected abstract void SetCurrentNormalizedValue(double cv);
    
        /// <summary>
        /// Based on the current position calculates what angle the current mouse
        /// position represents relative to the rotation point of the needle
        /// </summary>
        /// <param name="_currentPoint">Current point</param>
        /// <returns>Angle in degrees</returns>
        protected abstract double CalculateRotationAngle(Point currentPoint);
    
    }
}