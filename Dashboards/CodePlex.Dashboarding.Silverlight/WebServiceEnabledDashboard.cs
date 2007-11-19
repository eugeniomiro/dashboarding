using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodePlex.Dashboarding.Silverlight.Services;

namespace CodePlex.Dashboarding.Silverlight
{
    public abstract class WebServiceEnabledDashboard: Dashboard
    {
        private object _webServiceObject;
        private WebServiceDefinition _webServiceDefinition;

        private WebserviceControl _serviceControl;

        public String WebServiceLocalIdentifier { get; set; }
       

        public WebServiceEnabledDashboard(string manifestResource): base(manifestResource)
        {
        }

        public bool IsWebserviceEnabled
        {
            get { return ServiceControl != null && WebServiceLocalIdentifier != null; }
        }

        protected override void DashboardLoaded(object sender, EventArgs e)
        {
            base.DashboardLoaded(sender, e);
            if (IsWebserviceEnabled)
            {
                _webServiceDefinition = WebServiceManager.Retrieve(WebServiceLocalIdentifier);
                _serviceControl.Timer.Duration = new Duration(new TimeSpan(0, 0, _webServiceDefinition.RefreshPeriod));
                _serviceControl.Timer.Completed += new EventHandler(DoWebServiceCall);

                DoWebServiceCall(null, null);
            }
        }

    

        private void DoWebServiceCall(object sender, EventArgs e)
        {
            _webServiceObject = _webServiceDefinition.BeginAsyncCall(new AsyncCallback(WebServiceComplete));
        }

        private void WebServiceComplete(IAsyncResult async)
        {
            int newValue = _webServiceDefinition.EndAsyncCall(async, _webServiceObject);
            Value = newValue;
            IDisposable disp = _webServiceObject as IDisposable;

            // if it implements IDisposable, let's be a good citizen
            if (disp != null)
            {
                disp.Dispose();
            }
            _webServiceObject = null;


            _serviceControl.Timer.Begin();
        }

    

        protected WebserviceControl ServiceControl
        {
            get 
            {
                if (_serviceControl == null)
                {
                    _serviceControl = Root.FindName("_webServiceControl") as WebserviceControl;
                }
                return _serviceControl; 
            }
            set { _serviceControl = value; }
        }

    }
}
