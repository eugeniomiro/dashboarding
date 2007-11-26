using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace CodePlex.Dashboarding.Silverlight.Tooltips
{
    public abstract class Tooltip : Control
    {
        private string _text;
        private Canvas _parent;
        private double _x;
        private double _y;

        public abstract void Animate();

        public Tooltip(Canvas parent, string text, string manifestResource)
        {
            Loaded += new EventHandler(TooltipLoaded);
            
            _text = text;
            _parent = parent;
            Visibility = Visibility.Collapsed;
            _parent.Children.Add(this);

            parent.MouseEnter += new MouseEventHandler(Show);
            parent.MouseLeave += new EventHandler(Hide);
            parent.MouseMove += new MouseEventHandler(Move);
 
            using (Stream s = this.GetType().Assembly.GetManifestResourceStream(manifestResource))
            {
                Root = this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            }

        }

        protected FrameworkElement Root { get; set; }

        protected bool IsLoaded { get; set; }

        void Show(object sender, MouseEventArgs e)
        {
            if (Text != null && Text != "")
            {
                Visibility = Visibility.Visible;
                SetFromMousePostition(sender, e);
            }
        }


        void Move(object sender, MouseEventArgs e)
        {
            SetFromMousePostition(sender, e);
        }

        void Hide(object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }



        protected void TooltipLoaded(object sender, EventArgs e)
        {
            IsLoaded = true;
            BaseAnimate();
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                SetTextBlockContent();

            }
        }




        private void BaseAnimate()
        {
            if (IsLoaded)
            {
                Animate();
            }
        }


        private void SetFromMousePostition(object sender, MouseEventArgs e)
        {
            _x = e.GetPosition(_parent).X;
            _y = e.GetPosition(_parent).Y;

            Canvas tooltip = Root.FindName("_tooltipCanvas") as Canvas;

            double canvasWidth = (double)_parent.GetValue(Canvas.WidthProperty);
            double canvasHeight = (double)_parent.GetValue(Canvas.HeightProperty);

            double tooltipWidth = (double)tooltip.GetValue(Canvas.WidthProperty);
            double tooltipHeight = (double)tooltip.GetValue(Canvas.HeightProperty);



            if (_x + tooltipWidth > canvasWidth)
            {
                _x = canvasWidth - tooltipWidth;
            }

            if (_y + tooltipHeight > canvasHeight)
            {
                _y = canvasHeight - tooltipHeight;
            }


            tooltip.SetValue<double>(Canvas.LeftProperty, _x);
            tooltip.SetValue<double>(Canvas.TopProperty, _y);

            SetTextBlockContent();


            tooltip.Visibility = Visibility.Visible;
            BaseAnimate();
        }

        private void SetTextBlockContent()
        {
            TextBlock block = Root.FindName("_toolTipBlock") as TextBlock;
            if (block != null)
            {
                block.Text = Text;
            }
        }

    }
}
