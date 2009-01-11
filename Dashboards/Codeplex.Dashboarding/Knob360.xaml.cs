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
    /// Dial 360 with a control knob to drag not a needle
    /// </summary>
    public partial class Knob360 : BidirectionalDashboard
    {
        /// <summary>
        /// Constructs a Knob360
        /// </summary>
        public Knob360()
        {
            InitializeComponent();
            SetValue(FaceColorRangeProperty, new ColorPointCollection());
            SetValue(NeedleColorRangeProperty, new ColorPointCollection());
            RegisterGrabHandle(_indicator);
        }


        #region FaceColorRange property

        /// <summary>
        /// Dependancy property for the FaceColor attached property
        /// </summary>
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(Knob360), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));

        /// <summary>
        /// Specifies the face color at points in the range. A single color point with
        /// a value of 0 specifies the color for all
        /// </summary>
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
        private static void FaceColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Knob360 instance = dependancy as Knob360;
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
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(Knob360), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));

        /// <summary>
        /// Specifies what color the needle is a various point is the range
        /// </summary>
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
            Knob360 instance = dependancy as Knob360;
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
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(Knob360), new PropertyMetadata(new PropertyChangedCallback(TextColorChanged)));

        /// <summary>
        /// The color of the text used to show the percentage
        /// </summary>
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
        private static void TextColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            Knob360 instance = dependancy as Knob360;
            if (instance != null)
            {
                for (int i = 0; i <= 11; i++)
                {
                    TextBlock tb = instance.LayoutRoot.FindName("_txt" + i) as TextBlock;
                    if (tb != null)
                    {
                        tb.Foreground = new SolidColorBrush(instance.TextColor);
                    }
                }
                instance.OnPropertyChanged("TextColor");
            }
        }

        #endregion

        #region TextVisibility property
        /// <summary>
        /// The dependancy property for theTextVisibility attached property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(Knob360), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));


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
            Knob360 instance = dependancy as Knob360;


            if (instance != null)
            {
                for (int i = 0; i <= 11; i++)
                {
                    TextBlock tb = instance.LayoutRoot.FindName("_txt" + i) as TextBlock;
                    if (tb != null)
                    {
                        tb.Visibility = instance.TextVisibility;
                    }
                }
                instance.OnPropertyChanged("TextVisibility");

            }
        }

        #endregion

        #region BiDirection
        /// <summary>
        /// Highlight the grab handle as the mouse is in
        /// </summary>
        protected override void ShowGrabHandle()
        {
            base.ShowGrabHandle();
            _grabHighlight.Fill = new SolidColorBrush(Color.FromArgb(0x4c, 0xde, 0xf0, 0xf6));
        }

        /// <summary>
        /// Stop the highlight of the grab handle the mouse is out
        /// </summary>
        protected override void HideGrabHandle()
        {
            base.HideGrabHandle();
            _grabHighlight.Fill = new SolidColorBrush(Colors.Transparent);
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

            if (cv < -150)
                cv = -150;
            if (cv > 150)
                cv = 150;
            CurrentNormalizedValue = (cv + 150) / 300;

            Animate();
        }

        private double CalculateRotationAngle(Point _currentPoint)
        {
            double opposite = _currentPoint.Y - (ActualHeight / 2);
            double adjacent = _currentPoint.X - (ActualWidth / 2);
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y <= (ActualHeight / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else if (_currentPoint.X < (ActualWidth / 2) && _currentPoint.Y <= (ActualHeight / 2))
            {
                // already done
            }
            else if (_currentPoint.X >= (ActualWidth / 2) && _currentPoint.Y > (ActualHeight / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else
            {
                //angleInDegrees = 360 + angleInDegrees;
            }

            angleInDegrees = (angleInDegrees - 90) % 360;


            return angleInDegrees;
        }

        #endregion



        /// <summary>
        /// Animates the dial moving the grab knob and needle
        /// </summary>
        protected override void Animate()
        {
            SetFaceColor();
            SetNeedleColor();

            _txt11.Text = String.Format("{0:000}", Value);

            if (!IsBidirectional || (IsBidirectional && !IsGrabbed))
            {               
                _needlePos.Value = (-150+(300*NormalizedValue))-2;
                _grabPos.Value =  (300*NormalizedValue) -10;
                _swipe.Begin();
            }
            else
            {
                TransformGroup tg = _needle.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, (-150+(300*CurrentNormalizedValue))-2);

                tg = _grabCanvas.RenderTransform as TransformGroup;
                tg.Children[2].SetValue(RotateTransform.AngleProperty, (300*CurrentNormalizedValue) -10);
            }
        }


        #region privates

        /// <summary>
        /// Set the face color from the range
        /// </summary>
        private void SetFaceColor()
        {

            ColorPoint c = FaceColorRange.GetColor(Value);
            if (c != null)
            {
                _colourRangeStart.Color = c.HiColor;
                _colourRangeEnd.Color = c.LowColor;
            }
        }

        /// <summary>
        /// Set the needle color from the range
        /// </summary>
        private void SetNeedleColor()
        {
            ColorPoint c = NeedleColorRange.GetColor(Value);
            if (c != null)
            {
                _needle.Fill = new SolidColorBrush(c.HiColor);
            }
        }

        #endregion

    }
}
