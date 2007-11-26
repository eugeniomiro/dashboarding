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
    public class BoardLevel : Control
    {
        public BoardLevel()
        {
            InitializeWebServices();
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("David.Black.Bashboarding.Dashboards.BoardLevel.xaml");
            this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
        }

        private void InitializeWebServices()
        {
            WebServiceDefinition noParams = new WebServiceDefinition
            {
                LocalIdentifier = "boardLevel.noParams",
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
