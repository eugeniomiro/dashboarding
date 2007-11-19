using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace CodePlex.Dashboarding.Silverlight.Common
{
    public class ColourRange
    {
        private string _defintion;
        private const string ErrorMessage = "Incorrect format for colour range. corrct format is: [def]* where def = lowerbound:Colour1,Colour2. For example [0:#FF562348,#FF239856][50:#349023,#FF569264]";
        private List<Bound> _bounds = new List<Bound>();

        public ColourRange(string definition)
        {
            _defintion = definition;
            Initialize();
        }

        private void Initialize()
        {
            string[] parts = _defintion.Split('[');
            foreach (string part in parts)
            {
                if (part.Trim().Length > 0)
                {
                    _bounds.Add(Bound.Create(part.Replace("]", "").Trim()));
                }
            }
            _bounds.Sort((a, b) => a.LowerBound.CompareTo(b.LowerBound));
        }

        public bool HasBounds
        {
            get { return _bounds.Count > 0; }
        }

        internal Bound GetBound(int percentage)
        {
            var bound = _bounds[0];
            foreach (var b in _bounds)
            {
                if (percentage > b.LowerBound)
                {
                    bound = b;
                }
            }
            return bound;
        }

    }


}
