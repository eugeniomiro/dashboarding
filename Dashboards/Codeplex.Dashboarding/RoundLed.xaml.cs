using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace Codeplex.Dashboarding
{
    /// <summary>
    /// A RoundLed control is a BinaryDashboard that shows the true false
    /// values as a round led. True and false colors can be set
    /// </summary>
    public partial class RoundLed : BinaryDashboard
    {
        /// <summary>
        /// Contructs a round LED control
        /// </summary>
        public RoundLed()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {
                PerformCommonBinaryAnimation(_true, _false, Timeline1);
        }

       
    }
}
