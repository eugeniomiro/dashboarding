using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Codeplex.Dashboarding;

namespace Casewise.Dashboarding.Tests
{
    public class SimpleMockDashboard : Dashboard
    {
        
        /// <summary>
        /// Has the animate method been called?
        /// </summary>
        public bool AnimateCalled { get; set; }


        protected override void Animate()
        {
            AnimateCalled = true;
        }

    }
}
