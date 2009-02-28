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
using System.Diagnostics.CodeAnalysis;



namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class BinaryDashboardTests
    {
        [TestMethod]
        [Description("We should get a property change notification when accessing via a property directly")]
        public void IsTruePropertyChangeNotification()
        {
            var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("IsTrue" == e.PropertyName);
                }
            };
            d.IsTrue = !d.IsTrue;
            Assert.IsTrue(passed);
        }

        [TestMethod]
        [Description("We should get a property change notification when accessing via a dependacy property and SetValue")]
        public void IsTrueDependancyPropertyChangeNotification()
        {
            var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("IsTrue" == e.PropertyName);
                }
            };
            d.SetValue(BinaryDashboard.IsTrueProperty, !d.IsTrue);
            Assert.IsTrue(passed);
        }

        [TestMethod]
        [Description("Setting the property should change the value")]
        public void IsTrueProperty()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            
            d.SetValue(BinaryDashboard.IsTrueProperty, true);
            Assert.IsTrue(d.IsTrue);
            
            d.SetValue(BinaryDashboard.IsTrueProperty, false);
            Assert.IsFalse(d.IsTrue);

            d.IsTrue = true;
            Assert.IsTrue(d.IsTrue);

            d.IsTrue = false;
            Assert.IsFalse(d.IsTrue);

        }

        [TestMethod]
        [Description("We should get a property change notification when accessing via a property directly")]
        public void TrueColorPropertyChangeNotification()
        {
            var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("TrueColor" == e.PropertyName);
                }
            };
            d.TrueColor = new ColorPoint();
            Assert.IsTrue(passed);
        }

        [TestMethod]
        [Description("We should get a property change notification when accessing via a dependacy property and SetValue")]
        public void TrueColorDependancyPropertyChangeNotification()
        {
             var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("TrueColor" == e.PropertyName);
                }
            };
            d.SetValue(BinaryDashboard.TrueColorProperty,  new ColorPoint());
            Assert.IsTrue(passed);
        }
        

        [TestMethod]
        [Description("Animate is called when the property changes")]
        public void TrueColorPropertyChangeCallsAnimate()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.TrueColor = new ColorPoint();
            Assert.IsTrue(d.AnimateCalled);

        }




        [TestMethod]
        [Description("We should get a property change notification when accessing via a property directly")]
        public void FalseColorPropertyChangeNotification()
        {
            var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("FalseColor" == e.PropertyName);
                }
            };
            d.FalseColor = new ColorPoint();
            Assert.IsTrue(passed);
        }

        [TestMethod]
        [Description("We should get a property change notification when accessing via a dependacy property and SetValue")]
        public void FalseColorDependancyPropertyChangeNotification()
        {
            var passed = false;
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("FalseColor" == e.PropertyName);
                }
            };
            d.SetValue(BinaryDashboard.FalseColorProperty,  new ColorPoint());
            Assert.IsTrue(passed);
        }

        [TestMethod]
        [Description("Animate is called when the property changes")]
        public void FalseColorPropertyChangeCallsAnimate()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.FalseColor = new ColorPoint();
            Assert.IsTrue(d.AnimateCalled);
        }

        [TestMethod]
        [Description("Setting the value is reflected in the IsTrue property")]
        public void SetValueMinMaxOnDefault()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.Value = 1;
            Assert.IsFalse(d.IsTrue);
            d.Value = 100;
            Assert.IsTrue(d.IsTrue);
        }

        [TestMethod]
        [Description("Setting the value is reflected in the IsTrue property")]
        public void SetValueMinMaxGtZero()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.Minimum = 200;
            d.Maximum = 3000;
            d.Value = 201;
            Assert.IsFalse(d.IsTrue);
            d.Value = 2999;
            Assert.IsTrue(d.IsTrue);
        }

        [TestMethod]
        [Description("Setting the value is reflected in the IsTrue property")]
        public void SetValueMinMaxLtZero()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.Minimum = -200;
            d.Maximum = -100;
            d.Value = -200;
            Assert.IsFalse(d.IsTrue);
            d.Value = -100;
            Assert.IsTrue(d.IsTrue);
        }

        [TestMethod]
        [Description("Setting the value is reflected in the IsTrue property")]
        public void SetValueBelowMinIsFalse()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.Minimum = 200;
            d.Maximum = 3000;
            d.Value = 32;
            Assert.IsFalse(d.IsTrue);
        }

        [TestMethod]
        [Description("Setting the value is reflected in the IsTrue property")]
        public void SetValueBelowAboveMax()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            d.Minimum = 200;
            d.Maximum = 3000;
            d.Value = 3200;
            Assert.IsTrue(d.IsTrue);
        }


        [TestMethod]
        [Description("The color specified should be stored")]
        public void TrueColorIsStored()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            ColorPoint cp = new ColorPoint();
            d.TrueColor = cp;
            Assert.AreSame(cp, d.TrueColor);

        }

        [TestMethod]
        [Description("The color specified should be stored away")]
        public void FalseColorIsStored()
        {
            SimpleMockBinaryDashboard d = new SimpleMockBinaryDashboard();
            ColorPoint cp = new ColorPoint();
            d.FalseColor = cp;
            Assert.AreSame(cp, d.FalseColor);
        }


    }
}
