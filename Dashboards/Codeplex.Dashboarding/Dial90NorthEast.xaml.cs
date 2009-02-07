﻿using System;
using System.Windows;

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A quarter of a circle dial that sweeps through 90 degrees upper right quadrant
    /// </summary>
    public partial class Dial90NorthEast : Dial90
    {
        /// <summary>
        /// Constructs a top left 90 degree dial
        /// </summary>
        public Dial90NorthEast()
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

            double opposite = ActualHeight - _currentPoint.Y;
            double adjacent = _currentPoint.X;
            double tan = opposite / adjacent;
            double angleInDegrees = Math.Atan(tan) * (180.0 / Math.PI);

            if (_currentPoint.X < 0)
            {
                angleInDegrees = 90;
            }
            return angleInDegrees;
        }


        /// <summary>
        /// Calculate the rotation angle from the normalised actual value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected override double CalculatePointFromNormalisedValue()
        {
            return 90 - (NormalizedValue * 90);
        }


        /// <summary>
        /// Calculate the rotation angle from the normalised current value
        /// </summary>
        /// <returns>angle in degrees to position the transform</returns>
        protected override double CalculatePointFromCurrentNormalisedValue()
        {
            return 90 - (CurrentNormalizedValue * 90);
        }
    }
}