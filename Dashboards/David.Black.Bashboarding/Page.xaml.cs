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
using David.Black.Bashboarding.Dashboards;

namespace David.Black.Bashboarding
{
    public partial class Page : Canvas
    {
        private const string UnselectedForeColour = "#FFF8C846";
        private const string SelectedForeColour = "#FF46C8F8";

        public Page()
        {
            InitializeComponentWebServiceDefinitions();
        }

        public void Page_Loaded(object o, EventArgs e)
        {
            InitializeComponent();
            InitializeMouseEvents(_btnApplications);
            InitializeMouseEvents(_btnBoardLevel);
            InitializeMouseEvents(_btnManagement);
            InitializeMouseEvents(_btnNetwork);
            InitializeMouseEvents(_btnProduction);
            InitializeMouseEvents(_btnServers);

            _btnApplications.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new Applications()); });
            _btnBoardLevel.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new BoardLevel()); });
            _btnManagement.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new Management()); });
            _btnNetwork.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new Network()); });
            _btnProduction.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new Production()); });
            _btnServers.MouseLeftButtonUp += new MouseEventHandler((s, ea) => { ShowControl(new Servers()); });
        }

 


        private void ShowControl(Control control)
        {

            _dashboardContainer.Children.Clear();
            _dashboardContainer.Children.Add(control);

            double w = (double) _dashboardContainer.GetValue(Canvas.WidthProperty);
            double h = (double)_dashboardContainer.GetValue(Canvas.HeightProperty);

            control.SetValue<double>(Canvas.LeftProperty, 0);
            control.SetValue<double>(Canvas.TopProperty, 0);
            control.SetValue<double>(Canvas.WidthProperty,w);
            control.SetValue<double>(Canvas.HeightProperty, h);

        }

        private void InitializeMouseEvents(TextBlock button)
        {
            button.MouseEnter += new MouseEventHandler(HighlightButton);
            button.MouseLeave += new EventHandler(RemoveHighight);
        }

        void RemoveHighight(object sender, EventArgs e)
        {
            TextBlock block = sender as TextBlock;
            if (block != null)
            {
                _buttonBack.Visibility = Visibility.Collapsed;
            }
            
        }

        void HighlightButton(object sender, MouseEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            if (block != null)
            {
                double left = (double)block.GetValue(Canvas.LeftProperty);
                double top = (double)block.GetValue(Canvas.TopProperty);

                _buttonBack.SetValue<double>(Canvas.LeftProperty, left-10);
                _buttonBack.SetValue<double>(Canvas.TopProperty, top+2);

                _buttonBack.Visibility = Visibility.Visible;
            }
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







    }
}
