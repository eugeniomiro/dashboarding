using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Services
{
    public class WebserviceControl : Control
    {
        private Storyboard _timer;
        private Canvas _mask;
        private FrameworkElement _root;

        public WebserviceControl()
        {
            using (System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("CodePlex.Dashboarding.Silverlight.Services.WebserviceControl.xaml"))
            {
                _root = this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            }
        }

        internal Storyboard Timer
        {
            get
            {
                if (_timer == null)
                {
                    _timer = _root.FindName("timer") as Storyboard;
                }
                return _timer;
            }
        }

        internal Canvas Mask
        {
            get
            {
                if (_mask == null)
                {
                    _mask = _root.FindName("") as Canvas;
                }
                return _mask;
            }
        }

    }
}
