#region Copyright 2008 David Black

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

#endregion

using System;
using System.Windows;
using System.Windows.Controls;

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
            InitializeDial90();
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
        
        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected override Grid ResourceRoot
        {
            get { return LayoutRoot; }
        }

    }
}
