﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodePlex.Dashboarding.Silverlight.Common;
using CodePlex.Dashboarding.Silverlight.Tooltips;
using CodePlex.Dashboarding.Silverlight.Services;
using System.Diagnostics;

namespace CodePlex.Dashboarding.Silverlight
{
    public abstract class Dashboard : Control
    {
        #region private members
        
        private Tooltip _tooltip;
        private object _value;
        private Canvas _mainCanvas;
        private ColourRange _range;

        #endregion

        public Dashboard(string manifestResource)
        {
            using (System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream(manifestResource))
            {
                Root = this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            }
            Loaded += new EventHandler(DashboardLoaded);
        }

        public string ColourRange
        {
            set { _range = new ColourRange(value); }
        }

        public FrameworkElement Root { get; set; }

        public string Tooltip { get; set; }

        public bool IsLoaded { get; set; }

     

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Animate();
            }
        }

        public int ValueAsInteger
        {
            get 
            {
                Debug.Assert(_value.GetType().IsAssignableFrom(typeof(System.Int32)));
                return (int) _value; 
            }
        }

        public bool ValueAsBool
        {
            get
            {
                Debug.Assert(_value.GetType().IsAssignableFrom(typeof(System.Boolean)));
                return (bool)_value;
            }
        }

        protected Canvas MainCanvas
        {
            get
            {
                if (_mainCanvas == null)
                {
                    _mainCanvas = Root.FindName("_mainCanvas") as Canvas;
                }
                return _mainCanvas;
            }
        }

        public string PercentageText
        {
            get
            {
                TextBlock tb = Root.FindName("_percentage") as TextBlock;
                if (tb != null)
                {
                    return tb.Text;
                }
                return null;
            }
            set
            {
                TextBlock tb = Root.FindName("_percentage") as TextBlock;
                if (tb != null)
                {
                    tb.Text = value;
                }
            }
        }



        protected virtual void DashboardLoaded(object sender, EventArgs e)
        {
            IsLoaded = true;
            _tooltip = TooltipFactory.Create(MainCanvas, Tooltip);
            _tooltip.Text = Tooltip;
            Animate();
        }

        protected void SetColourFromRange()
        {
            if (_range != null)
            {
                Bound b = _range.GetBound(GetValueForSettingRange());
                for (int i = 0; i < 20; i++)
                {
                    GradientStop hiStop = Root.FindName("rangeHighColour" + i) as GradientStop;
                    if (hiStop != null)
                    {
                        hiStop.Color = b.HiColour;
                    }
                    GradientStop lowStop = Root.FindName("rangeLowColour" + i) as GradientStop;
                    if (lowStop != null)
                    {
                        lowStop.Color = b.LowColour;
                    }
                }
            }
        }

        private int GetValueForSettingRange()
        {
            if (typeof(bool) == Value.GetType())
            {
                return ValueAsBool ? 1 : 0;
            }
            return ValueAsInteger;
        }

        protected abstract void Animate();
    }
}
