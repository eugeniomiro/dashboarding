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
    /// A 180 degree dial where the pointer sweeps from North to south
    /// </summary>
    public partial class Dial180East : Dial180
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dial180East"/> class.
        /// </summary>
        public Dial180East()
        {
            InitializeComponent();
            InitializeDial180();
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

        /// <summary>
        /// Determines the angle of the needle based on the mouse 
        /// position.
        /// </summary>
        /// <param name="currentPoint">Mouse position</param>
        /// <returns>The angle in degrees</returns>
        protected override double CalculateRotationAngle(Point currentPoint)
        {
            double opposite = currentPoint.Y - (172 / 2);
            double adjacent = currentPoint.X - (ActualWidth / 2);
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (currentPoint.X >= (ActualWidth / 2) && currentPoint.Y <= (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }
            else if (currentPoint.X < (ActualWidth / 2) && currentPoint.Y <= (172 / 2))
            {
                // already done
            }
            else if (currentPoint.X >= (ActualWidth / 2) && currentPoint.Y > (172 / 2))
            {
                angleInDegrees = 180 + angleInDegrees;
            }

            return angleInDegrees;
        }

        /// <summary>
        /// Calculate the rotation angle from the normalized actual value
        /// </summary>
        /// <returns>
        /// angle in degrees to position the transform
        /// </returns>
        protected override double CalculatePointFromNormalisedValue()
        {
            return (NormalizedValue * 180);
        }

        /// <summary>
        /// Calculate the rotation angle from the normalized current value
        /// </summary>
        /// <returns>
        /// angle in degrees to position the transform
        /// </returns>
        protected override double CalculatePointFromCurrentNormalisedValue()
        {
            return (CurrentNormalizedValue * 180);
        }
    }
}
