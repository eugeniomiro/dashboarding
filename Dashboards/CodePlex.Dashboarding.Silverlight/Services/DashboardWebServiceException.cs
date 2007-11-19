using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodePlex.Dashboarding.Silverlight.Services
{
    
    public class DashboardWebServiceException : Exception
    {
        public DashboardWebServiceException() { }
        public DashboardWebServiceException(string message) : base(message) { }
        public DashboardWebServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
