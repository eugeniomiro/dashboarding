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
    /// ColorPoints allow you to set the color of an item at a start point in the range
    /// of values. A ColorPointCollection aggregates these ColorPoints.
    /// </summary>
    public class ColorPointCollection : List<ColorPoint>
    {
        /// <summary>
        /// Constructs a ColourPointCollection
        /// </summary>
        public ColorPointCollection()
        {

        }

        internal ColorPoint GetColor(double position)
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
