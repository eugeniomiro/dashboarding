using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using WpfApplication.Demonstrators;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IDemonstrateDials> _demonstrations = new List<IDemonstrateDials>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FindDemonstrators();
            InitializeDemonstaratorSelection();
        }

        /// <summary>
        /// Initializes the demonstarator selection control.
        /// </summary>
        private void InitializeDemonstaratorSelection()
        {
            _select.ItemsSource = _demonstrations;
            _select.SelectionChanged += new SelectionChangedEventHandler(DemoSelected);
        }

        /// <summary>
        /// A demo has been selected display it
        /// /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        void DemoSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _contentPanel.Children.Clear();
                _contentPanel.Children.Add(((IDemonstrateDials)e.AddedItems[0]).Create());
            }
        }

        /// <summary>
        /// Finds the demonstrators registered by attribute.
        /// </summary>
        private void FindDemonstrators()
        {
            var types = from p in Assembly.GetExecutingAssembly().GetTypes()
                        where (p.GetInterface("IDemonstrateDials") != null)
                            select p;
            _demonstrations = (from p in types 
                                orderby p.Name
                                select Activator.CreateInstance(p) as IDemonstrateDials).ToList();
            _demonstrations.Sort((a,b) => a.DemoName.CompareTo(b.DemoName));
        }
    }
}
