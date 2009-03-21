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
using System.Diagnostics.CodeAnalysis;


namespace Codeplex.Dashboarding
{
    /// <summary>
    /// The performance monitor control was inspired by the graph in the 
    /// performance tab of the windows TaskManager. It maintains a historical
    /// high and displays a filled shaded graph.
    /// 
    /// <para>There are many color properties to allow you to customize the
    /// display of the control</para>
    /// </summary>
    public partial class PerformanceMonitor : Dashboard
    {
        /// <summary>
        /// Constructs a PerformanceMonitior control
        /// </summary>
        public PerformanceMonitor()
        {
            InitializeComponent();
            GraphLine = Colors.Cyan;
            GridLine = Colors.White;
            Axis = Colors.Green;
            ValueTextColor = Colors.Green;
            GraphFillTo = Colors.Gray;
            GraphFillFrom = Colors.DarkGray;

            SizeChanged += new SizeChangedEventHandler(PerformanceMonitor_SizeChanged);
            Loaded += new RoutedEventHandler(PerformanceMonitor_Loaded);
        }

        void PerformanceMonitor_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColours();
            StoreValue();
            DrawLine();
            UpdateMinMxValues();
        }

        /// <summary>
        /// Handles the SizeChanged event of the PerformanceMonitor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        void PerformanceMonitor_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridRedrawRequired = true;
            UpdateColours();
        }


        #region dependancy properties

        #region GridLine color Dependancy property

        /// <summary>
        /// The grid color has changed, update it as soon as the canvas is ready
        /// </summary>
        private bool GridRedrawRequired { get; set; }

        /// <summary>
        /// Dependancy property for GridLine color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "GridLine")]
        public static readonly DependencyProperty GridLineProperty =
            DependencyProperty.Register("GridLine",
                                typeof(Color), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(GridLineColorChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "GridLine")]
        public Color GridLine
        {
            get { return (Color)GetValue(GridLineProperty); }
            set
            {
                SetValue(GridLineProperty, value);
            }
        }

        /// <summary>
        /// Our color has changed possibly via the GridLineProperty ot via a SetValue directly
        /// on the dependancy property. We change the color to the new value
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        private static void GridLineColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.GridRedrawRequired = true;
                instance.UpdateGridLineColor();
            }
        }

        #endregion

        #region Axis color Dependancy property

        
        /// <summary>
        /// Dependancy property for GridLine color
        /// </summary>
        public static readonly DependencyProperty AxisProperty =
            DependencyProperty.Register("Axis",
                                typeof(Color), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(AxisChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        public Color Axis
        {
            get { return (Color)GetValue(AxisProperty); }
            set
            {
                SetValue(AxisProperty, value);
            }
        }

        /// <summary>
        /// Our color has changed possibly via the GridLineProperty ot via a SetValue directly
        /// on the dependancy property. We change the color to the new value
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        private static void AxisChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateAxisColor();
            }
        }

        #endregion


       


        #region GraphLine color Dependancy property


        /// <summary>
        /// Dependancy property for GraphLine color
        /// </summary>
        public static readonly DependencyProperty GraphLineProperty =
            DependencyProperty.Register("GraphLine",
                                typeof(Color), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(GraphLineColorChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        public Color GraphLine
        {
            get { return (Color)GetValue(GraphLineProperty); }
            set
            {
                SetValue(GraphLineProperty, value);
            }
        }

        /// <summary>
        /// Our color has changed possibly via the GraphLineProperty or via a SetValue directly
        /// on the dependancy property. We change the color to the new value
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Color", Justification = "We support U.S. naming in a British project")]
        private static void GraphLineColorChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateGraphLineColors();
            }
        }

        #endregion

        #region GraphFillFrom color Dependancy property


        /// <summary>
        /// Dependancy property for GraphLine color
        /// </summary>
        public static readonly DependencyProperty GraphFillFromProperty =
            DependencyProperty.Register("GraphFillFrom",
                                typeof(Color), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(GraphFillFromChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        public Color GraphFillFrom
        {
            get { return (Color)GetValue(GraphFillFromProperty); }
            set
            {
                SetValue(GraphFillFromProperty, value);
            }
        }

        /// <summary>
        /// Our color has changed possibly via the GraphLineProperty or via a SetValue directly
        /// on the dependancy property. We change the color to the new value
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        private static void GraphFillFromChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateGraphFill();
            }
        }

       
        #endregion

        #region GraphFillTo color Dependancy property


        /// <summary>
        /// Dependancy property for GraphLine color
        /// </summary>
        public static readonly DependencyProperty GraphFillToProperty =
            DependencyProperty.Register("GraphFillTo",
                                typeof(Color), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(GraphFillToChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        public Color GraphFillTo
        {
            get { return (Color)GetValue(GraphFillToProperty); }
            set
            {
                SetValue(GraphFillToProperty, value);
            }
        }

        /// <summary>
        /// Our color has changed possibly via the GraphLineProperty or via a SetValue directly
        /// on the dependancy property. We change the color to the new value
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        private static void GraphFillToChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateGraphFill();
            }
        }


        #endregion


        #region HistoricalValues Dependancy property


        /// <summary>
        /// Dependancy property for Historical values
        /// </summary>
        public static readonly DependencyProperty HistoricalValuesProperty =
            DependencyProperty.Register("HistoricalValues",
                                typeof(List<double>), typeof(PerformanceMonitor),
                                new PropertyMetadata(new PropertyChangedCallback(HistoricalValuesChanged)));

        /// <summary>
        /// The color of the grid lines on the background of the graph
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is bound to xaml and the colection does change!")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<double> HistoricalValues
        {
            get { return (List<double>)GetValue(HistoricalValuesProperty); }
            set
            {
                SetValue(HistoricalValuesProperty, value);
            }
        }

        /// <summary>
        /// Initializes the control to a set of historical values to pre form a graph.
        /// </summary>
        /// <param name="dependancy">the PerformanceMonitor</param>
        /// <param name="args">old value and new value</param>
        private static void HistoricalValuesChanged(DependencyObject dependancy, DependencyPropertyChangedEventArgs args)
        {
            PerformanceMonitor instance = dependancy as PerformanceMonitor;
            if (instance != null && instance.DashboardLoaded)
            {
                instance.UpdateHistoricalValues();
            }
        }

        /// <summary>
        /// Initializes the control to a set of historical values to pre form a graph.
        /// </summary>
        private void UpdateHistoricalValues()
        {
            if (HistoricalValues != null && HistoricalValues.Count > 0)
            {
                _values.AddRange(HistoricalValues);
            }
        }

        #endregion




        #endregion


        /// <summary>
        /// Requires that the control hounours all appearance setting as specified in the
        /// dependancy properties (at least the supported ones). No dependancy property handling
        /// is performed until all dependancy properties are set and the control is loaded.
        /// </summary>
        protected override void ManifestChanges()
        {
            UpdateAxisColor();
            UpdateColours();
            UpdateGraphFill();
            UpdateGraphLineColors();
            UpdateGridLineColor();
            UpdateHistoricalValues();
            UpdateMinMxValues();
            UpdateTextColor();
            UpdateTextFormat();
            UpdateTextVisibility();

        }

        


        private int _historicalMax;
        private int _historicalMin;

        private List<double> _values = new List<double>();





        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {
            
                UpdateColours();
                StoreValue();
                DrawLine();
                UpdateMinMxValues();
            
        }

        /// <summary>
        /// Sets the colours.
        /// </summary>
        private void UpdateColours()
        { 
                UpdateGridLineColor();
                UpdateGraphLineColors();
                UpdateTextColor();
                UpdateAxisColor();
                UpdateGraphFill();           
        }

        /// <summary>
        /// Sets the graph fill.
        /// </summary>
        private void UpdateGraphFill()
        {
            rangeHighColour0.Color = GraphFillFrom;
            rangeLowColour0.Color = GraphFillTo;
        }


        /// <summary>
        /// Sets the color of the axis.
        /// </summary>
        private void UpdateAxisColor()
        {
            _vertAxis.Stroke = new SolidColorBrush(Axis);
            _horAxis.Stroke = new SolidColorBrush(Axis);

        }

        /// <summary>
        /// Set the visibiity of your text according to that of the TextVisibility property
        /// </summary>
        protected override void UpdateTextVisibility()
        {
            _lowWaterMark.Visibility = ValueTextVisibility;
            _highWaterMark.Visibility = ValueTextVisibility;

        }

        /// <summary>
        /// Update your text colors to that of the TextColor dependancy property
        /// </summary>
        protected override void UpdateTextColor()
        {
            _lowWaterMark.Foreground = new SolidColorBrush(ValueTextColor);
            _highWaterMark.Foreground = new SolidColorBrush(ValueTextColor);
        }

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected override void UpdateTextFormat()
        {
        }

        /// <summary>
        /// Sets the graph line colors.
        /// </summary>
        private void UpdateGraphLineColors()
        {
            _path.Stroke = new SolidColorBrush(GraphLine);
        }

        #region draw grid
        private void UpdateGridLineColor()
        {
            if (!GridRedrawRequired || _canvas.ActualHeight == 0 || _canvas.ActualHeight == 0)
                return;

            if (_lines.Count > 0)
            {
                foreach (Line line in _lines)
                {
                    _canvas.Children.Remove(line);
                }
                _lines.Clear();
            }

            
            DrawLines( 10, 100);
            DrawLines(50, 100);
            DrawLines(100, 100);
            Canvas.SetZIndex(_path, 1000);
            GridRedrawRequired = false;
        }

        private List<Line> _lines = new List<Line>();

        private void DrawLines( int spacing, int maxSpacing)
        {

            double lineY = 0;
            double remainder = _canvas.ActualHeight % maxSpacing;
            if (remainder > 0)
            {
                lineY = -(remainder / 2);
            }


            while (lineY <= _canvas.ActualHeight)
            {
                Line l = new Line { X1 = 0, Y1 = lineY, X2 = _canvas.ActualWidth, Y2 = lineY, Opacity = 0.15, Stroke = new SolidColorBrush(GridLine) };
                _canvas.Children.Add(l);
                lineY += spacing;
                _lines.Add(l);
                Canvas.SetZIndex(l, 0);
            }

            double lineX = 0;
            remainder = _canvas.ActualWidth % maxSpacing;
            if (remainder > 0)
            {
                lineX = -(remainder / 2);
            }

            while (lineX <= _canvas.ActualWidth)
            {
                Line l = new Line { X1 = lineX, Y1 = 0, X2 = lineX, Y2 = _canvas.ActualHeight, Opacity = 0.15, Stroke = new SolidColorBrush(GridLine) };
                _canvas.Children.Add(l);
                lineX += spacing;
                _lines.Add(l);
                Canvas.SetZIndex(l, 0);
            }
        }


        #endregion


        private void UpdateMinMxValues()
        {
            _lowWaterMark.Text = _historicalMin.ToString();
            _highWaterMark.Text = _historicalMax.ToString();
        }

        private void DrawLine()
        {
            var _normalised = new List<double>();

            double ch = _canvas.ActualHeight;
            double cw = _canvas.ActualWidth;

            //if the line is from 0 to 99 in one pixel then back to 0 again the
            // path over extends and escapes the canvas, we clip to prevent this
            _canvas.Clip = new RectangleGeometry { Rect = new Rect(0, 0, cw, ch) };

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

  
            double startPoint = (cw - _values.Count);

            PathGeometry pg = new PathGeometry();
            pg.FillRule = FillRule.Nonzero;
            pg.Figures = new PathFigureCollection();
            PathFigure pf = new PathFigure();
            pf.IsClosed = true;
            
            pf.StartPoint = new Point(startPoint, ch);
            pf.Segments = new PathSegmentCollection();

           int idx = 0;


           for (int i = (int)startPoint; i < cw; i++)
            {
                double y = ch - (_normalised[idx] * ch);

               pf.Segments.Add(new LineSegment { Point = new Point(i + 1, y) });
                idx++;
            }

           pf.Segments.Add(new LineSegment { Point = new Point(cw, ch) });

            pg.Figures.Add(pf);

            _path.Data = pg;

            _path.Visibility = Visibility.Visible;


            
        }

  
        private void StoreValue()
        {
            if (_values.Count > 0 && _values.Count == _canvas.ActualWidth)
            {
                _values.RemoveAt(0);
            }
            _values.Insert(_values.Count, Value);
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
