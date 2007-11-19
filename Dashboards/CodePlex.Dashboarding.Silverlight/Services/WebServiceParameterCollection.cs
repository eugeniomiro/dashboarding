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

namespace CodePlex.Dashboarding.Silverlight.Services
{
    public class WebServiceParameterCollection: List<WebServiceParameter>
    {
        public object[] AsArgumentArray
        {
            get
            {
                var res = new List<object>();
                foreach (WebServiceParameter p in this)
                {
                    res.Add(p.Value);
                }
                return res.ToArray();
            }
        }
    }
}
