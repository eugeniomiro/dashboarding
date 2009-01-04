using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
#region Copyright 2008 David Black

/* -------------------------------------------------------------------------
 *     
 *  Copyright 2008 David Black
 *  
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *     
 *     http://www.apache.org/licenses/LICENSE-2.0
 *    
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 *  -------------------------------------------------------------------------
 */

#endregion


using System.Windows.Shapes;
using Microsoft.Silverlight.Testing.UnitTesting.Metadata.VisualStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeplex.Dashboarding;
using System.ComponentModel;
using Casewise.Dashboarding.Tests;

namespace Casewise.Dashboarding.PropertyChangeNotification.Tests
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
