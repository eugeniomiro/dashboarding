//-----------------------------------------------------------------------
// <copyright file="BidirectionalDial.cs" company="David Black">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Base class for all dials. We have the base properties common to all 
    /// of the controls (Needle color range etc)
    /// </summary>
    public abstract partial class BidirectionalDial : BidirectionalDashboard
    {
        #region public fields
        /// <summary>
        /// Dependancy property for the FaceColor attached property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty FaceColorRangeProperty =
            DependencyProperty.Register("FaceColorRange", typeof(ColorPointCollection), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(FaceColorRangeChanged)));
       
        /// <summary>
        /// The  Dependancy property for the NeedleColor attached property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty NeedleColorRangeProperty =
            DependencyProperty.Register("NeedleColorRange", typeof(ColorPointCollection), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(NeedleColorRangeChanged)));
        
        /// <summary>
        /// The Dependancy property for the TextColor attached property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(TextColorChanged)));
        
        /// <summary>
        /// The dependancy property for theTextVisibility attached property
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(BidirectionalDial), new PropertyMetadata(new PropertyChangedCallback(TextVisibilityPropertyChanged)));

        #endregion

        #region public Properties
        /// <summary>
        /// Gets or sets the face color range which dtermines the color of 
        /// the face of the dial at different value points.
        /// </summary>
        /// <value>The face color range.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is bound to xaml and the colection does change!")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
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
        /// Gets or sets the needle color range.
        /// </summary>
        /// <value>The needle color range.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is bound to xaml and the colection does change!")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
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
                Animate();
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

        #endregion

        #region protected properties
        /// <summary>
        /// Gets the shape used to highlight the fact that the mouse is in 
        /// drag control
        /// </summary>
        protected abstract Shape GrabHighlight { get; }

        #endregion

        #region protected methods

        /// <summary>
        /// Highlight the grab handle as the mouse is in
        /// </summary>
        protected override void ShowGrabHandle()
        {
            base.ShowGrabHandle();
            this.GrabHighlight.Fill = new SolidColorBrush(Color.FromArgb(0x4c, 0xde, 0xf0, 0xf6));
        }

        /// <summary>
        /// Stop the highlight of the grab handle the mouse is out
        /// </summary>
        protected override void HideGrabHandle()
        {
            base.HideGrabHandle();
            this.GrabHighlight.Fill = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Mouse is moving, move the diagram
        /// </summary>
        /// <param name="mouseDownPosition">origin of the drag</param>
        /// <param name="currentPosition">where the mouse is now</param>
        protected override void OnMouseGrabHandleMove(Point mouseDownPosition, Point currentPosition)
        {
            base.OnMouseGrabHandleMove(mouseDownPosition, currentPosition);
            double cv = this.CalculateRotationAngle(currentPosition);
            this.SetCurrentNormalizedValue(cv);
            Animate();
        }

        #endregion

        #region protected abstract methods

        /// <summary>
        /// Update your face color from the property value
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected abstract void SetFaceColor();

        /// <summary>
        /// Update your needle color from the property value
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected abstract void SetNeedleColor();

        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        protected abstract void SetTextColor();

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected abstract void SetTextVisibility();

        /// <summary>
        /// Based on the rotation angle, set the normalized current value
        /// </summary>
        /// <param name="cv">rotation angle</param>
        protected abstract void SetCurrentNormalizedValue(double cv);
    
        /// <summary>
        /// Based on the current position calculates what angle the current mouse
        /// position represents relative to the rotation point of the needle
        /// </summary>
        /// <param name="currentPoint">Current point</param>
        /// <returns>Angle in degrees</returns>
        protected abstract double CalculateRotationAngle(Point currentPoint);

        #endregion

        #region private methods
        /// <summary>
        /// The Text visibility property has cahaned..
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void TextVisibilityPropertyChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {
                instance.SetTextVisibility();
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
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {
                instance.SetTextColor();
                instance.OnPropertyChanged("TextColor");
            }
        }

        /// <summary>
        /// The FaceColorRange property changed, update the visuals
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        private static void FaceColorRangeChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            BidirectionalDial instance = dependancy as BidirectionalDial;
            if (instance != null)
            {
                instance.SetFaceColor();
                instance.OnPropertyChanged("FaceColorRange");
            }
        }

        /// <summary>
        /// The needle color range changed, handle it
        /// </summary>
        /// <param name="dependancy">The dependancy.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
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
    }
}