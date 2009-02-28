
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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Codeplex.Dashboarding.ColorPointCollection", MessageId = "Color")]
namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A ColorPoint allows you to set the color of an item at a start point. A 
    /// ColorPointCollection aggregates these ColorPoints.
    /// </summary>
     public class ColorPointCollection : ObservableCollection<ColorPoint>
    {
        /// <summary>
        /// Constructs a ColourPointCollection
        /// </summary>
        public ColorPointCollection()
        {

        }

        /// <summary>
        /// Get the Range with which to render an item at the specified 
        /// point in the range. If the value is below the value of the first color point we
        /// return the first color point, if it is greater than the last, we return that
        /// </summary>
        /// <param name="position">The value in the range at which you want to get 
        /// color to render an item</param>
        /// <returns>The color point for the psition</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color")]
        public ColorPoint GetColor(double position)
        {
            ColorPoint res = null;
            foreach (ColorPoint point in this)
            {
                if (position >= point.Value)
                    res = point;
            }

            // Have we underflowed?
            if (res == null && this.Count > 0)
            {
                return this[0];
            }


            return res;
        }
    }
}
