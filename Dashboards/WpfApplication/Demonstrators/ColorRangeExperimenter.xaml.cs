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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfApplication.Demonstrators
{
    /// <summary>
    /// Interaction logic for ColorRangeExperimenter.xaml
    /// </summary>
    public partial class ColorRangeExperimenter : UserControl
    {
        public ColorRangeExperimenter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// field for the range name
        /// </summary>
        private string _colorRangeName;

        /// <summary>
        /// Gets or sets the name of the range.
        /// </summary>
        /// <value>The name of the range.</value>
        public string ColorRangeName
        {
            get { return _colorRangeName; }
            set { _colorRangeName = value; _rangeName.Text = value; }
        }


    }
}
