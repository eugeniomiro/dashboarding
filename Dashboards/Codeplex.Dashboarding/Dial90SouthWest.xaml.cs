using System;
using System.Windows;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A quarter of a circle dial that sweeps through 90 degrees lower right quadrant
    /// </summary>
    public partial class Dial90SouthWest : Dial90
    {
        
        
        /// <summary>
        /// Constructs a top left 90 degree dial
        /// </summary>
        public Dial90SouthWest()
        {
            InitializeComponent();
            InitializeDial90(LayoutRoot);
        }
      

        /// <summary>
        /// Determines the angle of the needle based on the mouse 
        /// position.
        /// </summary>
        /// <param name="_currentPoint">Mouse position</param>
        /// <returns>The angle in degrees</returns>
        protected override double CalculateRotationAngle(Point _currentPoint)
        {

            double opposite = _currentPoint.Y ;
            double adjacent = (ActualWidth ) - (_currentPoint.X );
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (_currentPoint.Y < 10)
            {
                angleInDegrees = 0;
            }
            if (_currentPoint.X > 100)
            {
                angleInDegrees = 90;
            }

            _debug.Visibility = Visibility.Collapsed;
            _debug.Text = String.Format("{0:000}, {1:000}, {2:000},", angleInDegrees, _currentPoint.X, _currentPoint.Y);

             return   angleInDegrees;
        }


        /// <summary>
        /// Calculate the rotation angle from the normalised current value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected override double CalculatePointFromCurrentNormalisedValue()
        {
            return 90 - (CurrentNormalizedValue * 90);
        }


        /// <summary>
        /// Calculate the rotation angle from the normalised actual value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected override double CalculatePointFromNormalisedValue()
        {
            return 90 - (NormalizedValue * 90);
        }
    }
}
