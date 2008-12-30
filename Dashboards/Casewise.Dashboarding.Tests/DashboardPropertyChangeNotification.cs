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
using Microsoft.Silverlight.Testing.UnitTesting.Metadata.VisualStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeplex.Dashboarding;
using System.ComponentModel;

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class DashboardPropertyChangeNotification
    {
        [TestMethod]
        public void TestValueChangedPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged +=  delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Value" == e.PropertyName);
            };
            d.Value = 10;
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestMinimumChangedPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Minimum" == e.PropertyName);
            };
            d.Minimum = 10;
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestMaximumChangedPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Maximum" == e.PropertyName);
            };
            d.Maximum = 10;
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestValueChangedDependancyPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Value" == e.PropertyName);
            };
            d.SetValue(Dashboard.ValueProperty,10.0);
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestMinimumChangedDependancyPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Minimum" == e.PropertyName);
            };
            d.SetValue(Dashboard.MinimumProperty, 1.0);
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestMMaximumChangedDependancyPropertyNotification()
        {
            var passed = false;
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                passed = ("Maximum" == e.PropertyName);
            };
            d.SetValue(Dashboard.MaximumProperty, 1.0);
            Assert.IsTrue(passed);
        }
    
    }
}
