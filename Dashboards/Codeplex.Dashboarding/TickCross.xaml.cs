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
    /// Represents a boolean value, either as a Tick or cross for true and 
    /// false respecively. The user may specify the color of the tick and cross
    /// </summary>
    public partial class TickCross : BinaryDashboard
    {
        /// <summary>
        /// Constructs a TickCross dashboard
        /// </summary>
        public TickCross()
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
