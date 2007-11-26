using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Common
{
    internal class Bound
    {
        public int LowerBound { get; set; }
        public Color HiColour { get; set; }
        public Color LowColour { get; set; }

        internal static Bound Create(string fromXaml)
        {
            string[] parts = fromXaml.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Could not split bound from colours", "fromXaml");
            }
            int lb = Int32.Parse(parts[0].Trim());

            string[] colours = parts[1].Split(',');
            if (colours.Length != 2)
            {
                throw new ArgumentException("Could not split colours (missing comma??)", "fromXaml");
            }

            return new Bound
            {
                LowerBound = lb,
                HiColour = GetColour(colours[0]),
                LowColour = GetColour(colours[1])
            };
        }

        public static Color GetColour(string hex)
        {
            hex = hex.Replace("#", "").Trim();
            int asInteger = Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            byte a = (byte)((asInteger >> 24) & 0xFF);
            byte r = (byte)((asInteger >> 16) & 0xFF);
            byte g = (byte)((asInteger >> 8) & 0xFF);
            byte b = (byte)((asInteger) & 0xFF);
            return Color.FromArgb(a, r, g, b);
        }

    }
}
