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
    public class Network : Control
    {
        public Network()
        {
            InitializeWebServices();
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("David.Black.Bashboarding.Dashboards.Network.xaml");
            this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
        }

        private void InitializeWebServices()
        {
            WebServiceDefinition noParams = new WebServiceDefinition
            {
                LocalIdentifier = "network.noParams",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetCpuLoad",
                Parameters = new WebServiceParameterCollection
                {
                },
                RefreshPeriod = 1
            };

            WebServiceDefinition noParams2 = new WebServiceDefinition
            {
                LocalIdentifier = "network.noParams",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetSawTooth",
                Parameters = new WebServiceParameterCollection
                {
                },
                RefreshPeriod = 1
            };

            WebServiceDefinition noParams3 = new WebServiceDefinition
            {
                LocalIdentifier = "network.noParams",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetSquareWave",
                Parameters = new WebServiceParameterCollection
                {
                },
                RefreshPeriod = 1
            };

            WebServiceManager.Register(noParams);
        }

    }
}
