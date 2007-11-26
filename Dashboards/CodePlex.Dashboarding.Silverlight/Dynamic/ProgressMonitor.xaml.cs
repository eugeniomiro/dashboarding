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
using System.Linq;

namespace CodePlex.Dashboarding.Silverlight.Dynamic
{
    public class ProgressMonitor : WebServiceEnabledDashboard
    {
        private const int CanvasWidth = 200;
        private const int CanvasHeight = 100;



        private int _historicalMax = 0;
        private int _historicalMin = 0;

        private List<int> _values = new List<int>();
        

        public ProgressMonitor(): base("CodePlex.Dashboarding.Silverlight.Dynamic.ProgressMonitor.xaml")
        {
        }

        protected override void Animate()
        {
            if (Value != null && IsLoaded)
            {
                StoreValue();
                DrawLine();
                SetMinMxValues();
            }
        }

        private void SetMinMxValues()
        {
            TextBlock min = Root.FindName("_lowWaterMark") as TextBlock;
            TextBlock max = Root.FindName("_highWaterMark") as TextBlock;
            if (min != null)
            {
                min.Text = _historicalMin.ToString();
            }

            if (max != null)
            {
                max.Text = _historicalMax.ToString();
            }
        }

        private void DrawLine()
        {
            var _normalised = new List<double>();

            double max = _values.Max();
            double min = _values.Min();
            if (max > _historicalMax)
            {
                _historicalMax = (int)max;
            }
            if (min < _historicalMin)
            {
                _historicalMin = (int)min;
            }


            foreach (int val in _values)
            {
                if (_historicalMax == 0)
                {
                    _normalised.Add(0);
                }
                else
                {
                    _normalised.Add(((double)val) / _historicalMax);
                }
            }

            Path path = Root.FindName("_path") as Path;

            int startPoint = (CanvasWidth - _values.Count);

            PathGeometry pg = new PathGeometry();
            pg.FillRule = FillRule.Nonzero;
            pg.Figures = new PathFigureCollection();
            PathFigure pf = new PathFigure();
            pf.IsClosed = true;
            
            pf.StartPoint = new Point(startPoint, CanvasHeight);
            pf.Segments = new PathSegmentCollection();

           int idx = 0;


           for (int i = startPoint; i < CanvasWidth; i++)
            {
                double d = _normalised[idx];
                int tt = _values[idx];
                pf.Segments.Add(new LineSegment { Point = new Point(i + 1, CanvasHeight - (_normalised[idx] * CanvasHeight)) });

                idx++;
            }

           pf.Segments.Add(new LineSegment { Point = new Point(CanvasWidth, CanvasHeight) });

            pg.Figures.Add(pf);
            
            path.Data = pg;

            path.Visibility = Visibility.Visible;


            
        }

        public string InitialMaximumValue
        {
            set 
            {
                try
                {
                    _historicalMax = Int32.Parse(value);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not set ProgressMonitor Initial maximum, the string '" + value + "' did not parse to an integer"); 
                }
            }
        }

        private void StoreValue()
        {
            if (_values.Count == CanvasWidth)
            {
                _values.RemoveAt(0);
            }
            _values.Insert(_values.Count, ValueAsInteger);
        }
    }
}
