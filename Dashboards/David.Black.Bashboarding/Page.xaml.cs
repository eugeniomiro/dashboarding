using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodePlex.Dashboarding.Silverlight.Thermometers;
using System.Windows.Browser;
using CodePlex.Dashboarding.Silverlight.Services;

namespace David.Black.Bashboarding
{
    public partial class Page : Canvas
    {

        public Page()
        {
            InitializeComponentWebServiceDefinitions();
        }

        public void Page_Loaded(object o, EventArgs e)
        {
            InitializeComponent();
            ButtonClick.MouseLeftButtonDown += new MouseEventHandler(tb_MouseLeftButtonDown);
        }

        private void InitializeComponentWebServiceDefinitions()
        {
            WebServiceDefinition noParams = new WebServiceDefinition
            {
                LocalIdentifier = "noParams",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetDashboardValueNoParams",
                Parameters = new WebServiceParameterCollection
                {
                },
                RefreshPeriod = 5
            };

            WebServiceDefinition oneParam = new WebServiceDefinition
            {
                LocalIdentifier = "oneParam",
                Class = typeof(dashboardRef.DashboardWebservice),
                MethodName = "GetDashboardValueSingleIntegerAsRange",
                Parameters = new WebServiceParameterCollection
                {
                    new WebServiceParameter { Name="range", Value = 60 }
                },
                RefreshPeriod = 5
            };



            WebServiceManager.Register(noParams);
            WebServiceManager.Register(oneParam);

  
        }



        


        void tb_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Random r = new Random();
            _rangeText.Value = r.Next(100);

            _rangeColour.Value = r.Next(100);


            _basic360Dial2.Value = r.Next(100);



            dashboardRef.DashboardWebservice reference = new dashboardRef.DashboardWebservice();
            _thinThermometer2.Value = reference.GetDashboardValueNoParams();
        }
    }
}
