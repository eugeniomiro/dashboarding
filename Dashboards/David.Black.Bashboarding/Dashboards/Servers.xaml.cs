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
    public class Servers : Control
    {
        public Servers()
        {
            InitializeWebServices();
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("David.Black.Bashboarding.Dashboards.Servers.xaml");
            this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
        }

        private void InitializeWebServices()
        {
            WebServiceManager.Register(
               new WebServiceDefinition
               {
                   LocalIdentifier = "servers.cpu",
                   Class = typeof(dashboardRef.DashboardWebservice),
                   MethodName = "GetCpuLoad",
                   Parameters = new WebServiceParameterCollection
                   {
                   },
                   RefreshPeriod = 1
               }
           );

             WebServiceManager.Register(
                new WebServiceDefinition
                {
                    LocalIdentifier = "servers.saw",
                    Class = typeof(dashboardRef.DashboardWebservice),
                    MethodName = "GetSawTooth",
                    Parameters = new WebServiceParameterCollection
                    {
                    },
                    RefreshPeriod = 1
                }
            );

             WebServiceManager.Register(
                 new WebServiceDefinition
                {
                    LocalIdentifier = "servers.square",
                    Class = typeof(dashboardRef.DashboardWebservice),
                    MethodName = "GetSquareWave",
                    Parameters = new WebServiceParameterCollection
                    {
                    },
                    RefreshPeriod = 1
                }
             );


        }

    }
}

