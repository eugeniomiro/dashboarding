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

namespace David.Black.Bashboarding.Dashboards
{
    public class Applications : Control
    {
        public Applications()
        {
            InitializeWebServices();
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("David.Black.Bashboarding.Dashboards.Applications.xaml");
            this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
        }

        private void InitializeWebServices()
        {
            WebServiceDefinition noParams = new WebServiceDefinition
            {
                LocalIdentifier = "applicatins.noParams",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetDashboardValueNoParams",
                Parameters = new WebServiceParameterCollection
                {
                },
                RefreshPeriod = 5
            };

            WebServiceManager.Register(noParams);
        }
    }
}
