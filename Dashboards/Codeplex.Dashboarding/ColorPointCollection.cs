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

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A ColorPoint allows you to set the color of an item at a start point. A 
    /// ColorPointCollection aggregates these ColorPoints.
    /// </summary>
    public class ColorPointCollection : List<ColorPoint>
    {
        /// <summary>
        /// Constructs a ColourPointCollection
        /// </summary>
        public ColorPointCollection()
        {

        }

        /// <summary>
        /// Get the Range with which to render an item at the specified 
        /// point in the range 
        /// </summary>
        /// <param name="position">The value in the range at which you want to get 
        /// color to render an item</param>
        /// <returns>The color point for the psition</returns>
        public ColorPoint GetColor(double position)
        {
            ColorPoint res = null;
            foreach (ColorPoint point in this)
            {
                if (position >= point.Value)
                    res = point;
            }
            return res;
        }
    }
}
