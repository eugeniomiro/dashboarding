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

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class DashboardMinMaxAndValue
    {
        [TestMethod ]
        [Description("Minimum > Maximum both positive should produce correct RealMinimum and RealMaximum values")]
        public void RealMaxMinZeroPlus()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 100;
            d.Maximum = 0;
            Assert.IsTrue(d.RealMinimum == 0);
            Assert.IsTrue(d.RealMaximum == 100);
        }


        [TestMethod]
        [Description("Minimum > Maximum one negative should produce correct RealMinimum and RealMaximum values")]
        public void RealMaxMinZeroMinus()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 0;
            d.Maximum = -100;
            Assert.IsTrue(d.RealMinimum == -100);
            Assert.IsTrue(d.RealMaximum == 0);
        }

        [TestMethod]
        [Description("Minimum == Maximum should produce identical RealMinimum and RealMaximum values")]
        public void RealMaxMinBothSame()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 1;
            d.Maximum = 1;
            Assert.IsTrue(d.RealMinimum == 1);
            Assert.IsTrue(d.RealMaximum == 1);
        }

        [TestMethod]
        [Description("Setting value > Maximum should leave Value == Maximum")]
        public void ValuePropertyRoundDownAboveMaxPos()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Value = 102;
            Assert.IsTrue(d.Value == 100);
        }

        [TestMethod]
        [Description("Setting value > Maximum should leave Value == Maximum")]
        public void ValuePropertyRoundDownAboveMaxNoneZero()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 103;
            d.Maximum = 204;
            d.Value = 9999;
            Assert.IsTrue(d.Value == 204);
        }

        [TestMethod]
        [Description("Setting value > Maximum (using negative max and min) should leave Value == Maximum")]
        public void ValuePropertyRoundDownAboveMaxNeg()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -102;
            d.Maximum = -2;
            d.Value = 102;
            Assert.IsTrue(d.Value == -2);
        }


        [TestMethod]
        [Description("Setting value < Minimum should leave Value == Minimum")]
        public void ValuePropertyRoundUpBelowMinPos()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 0;
            d.Maximum = 100;
            d.Value = -1;
            Assert.IsTrue(d.Value == 0);
        }

        [TestMethod]
        [Description("Setting value < Minimum should leave Value == Minimum")]
        public void ValuePropertyRoundUpBelowMinAboveZero()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 103;
            d.Maximum = 204;
            d.Value = 2;
            Assert.IsTrue(d.Value == 103);
        }

        [TestMethod]
        [Description("Setting value < Minimum should leave Value == Minimum")]
        public void ValuePropertyRoundDownBelowMinNeg()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -102;
            d.Maximum = -2;
            d.Value = -112;
            Assert.IsTrue(d.Value == -102);
        }


        [TestMethod]
        [Description("Setting the value property should lead to Animate() being called")]
        public void ValuePropertyCallsAnimate()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -102;
            d.Maximum = -2;
            Assert.IsFalse(d.AnimateCalled);
            d.Value = -112;
            Assert.IsTrue(d.AnimateCalled);
        }

        [TestMethod]
        [Description("Setting the value dependancy property via SetValue() should lead to Animate() being called")]
        public void ValueDependancyPropertyCallsAnimate()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -102;
            d.Maximum = -2;
            Assert.IsFalse(d.AnimateCalled);
            d.SetValue(Dashboard.ValueProperty,-112.0);
            Assert.IsTrue(d.AnimateCalled);
        }

        [TestMethod]
        [Description("The RealMaximum and RealMinimum values should always honour RealMinimum < RealMaximum even if Maximum < Minimum")]
        public void RealMaxMinDependancyPropertyZeroPlus()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 100.0);
            d.SetValue(Dashboard.MaximumProperty, 0.0);
            Assert.IsTrue(d.RealMinimum == 0.0);
            Assert.IsTrue(d.RealMaximum == 100.0);
        }


        [TestMethod]
        [Description("The RealMaximum and RealMinimum values should always honour RealMinimum < RealMaximum even if Maximum < Minimum")]
        public void RealMaxMinZeroDependancyPropertyMinus()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 0.0);
            d.SetValue(Dashboard.MaximumProperty, -100.0);

            Assert.IsTrue(d.RealMinimum == -100.0);
            Assert.IsTrue(d.RealMaximum == 0.0);
        }

        [TestMethod]
        [Description("The RealMaximum and RealMinimum values should always honour RealMinimum < RealMaximum even if Maximum == Minimum")]
        public void RealMaxMinDependancyPropertyBothSame()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 2.0);
            d.SetValue(Dashboard.MaximumProperty, 2.0);

            Assert.IsTrue(d.RealMinimum == 2);
            Assert.IsTrue(d.RealMaximum == 2);
        }

        [TestMethod]
        [Description("Setting the value property via SetValue and the dependancy property should round down Values >Maximum to Maximum")]
        public void ValueDependancyPropertyRoundDownAboveMaxPos()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 0.0);
            d.SetValue(Dashboard.MaximumProperty, 100.0);
            d.Value = 102;
            Assert.IsTrue(d.Value == 100);
        }

        [TestMethod]
        [Description("Setting the value property via SetValue and the dependancy property should round down Values >Maximum to Maximum")]
        public void ValueDependancyPropertyRoundDownAboveMaxNoneZero()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 103.0);
            d.SetValue(Dashboard.MaximumProperty, 204.0);

            d.Value = 9999;
            Assert.IsTrue(d.Value == 204);
        }

        [TestMethod]
        [Description("Setting the value property via SetValue and the dependancy property should round down Values >Maximum to Maximum")]
        public void ValueDependancyPropertyRoundDownAboveMaxNeg()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, -102.0);
            d.SetValue(Dashboard.MaximumProperty, -2.0);
            d.Value = 102;
            Assert.IsTrue(d.Value == -2);
        }


        [TestMethod]
        [Description("Setting the value property via the Value property should round down Values <inimum to Minimum")]
        public void ValueDependancyPropertyRoundUpBelowMinPos()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 0.0);
            d.SetValue(Dashboard.MaximumProperty, 100.0);
            d.Value = -1;
            Assert.IsTrue(d.Value == 0);
        }

        [TestMethod]
        [Description("Setting the value property via the Value property should round down Values <inimum to Minimum")]
        public void ValueDependancyPropertyRoundUpBelowMinAboveZero()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, 103.0);
            d.SetValue(Dashboard.MaximumProperty, 204.0);
            d.Value = 2;
            Assert.IsTrue(d.Value == 103);
        }

        [TestMethod]
        [Description("Setting the value property via the Value property should round down Values <inimum to Minimum")]
        public void ValueDependancyPropertyRoundDownBelowMinNeg()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.SetValue(Dashboard.MinimumProperty, -102.0);
            d.SetValue(Dashboard.MaximumProperty, -2.0);
            d.Value = -112;
            Assert.IsTrue(d.Value == -102);
        }


        [TestMethod]
        [Description("By default minimum if not set should be 0")]
        public void TestDefaultMinIsZero()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            Assert.AreEqual(0, d.Minimum);
        }


        [TestMethod]
        [Description("by default if not set Maximum should be 100")]
        public void TestDefaultMaxIsHundred()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            Assert.AreEqual(100, d.Maximum);
        }


        [TestMethod]
        [Description("By default 0 should normalize to 0 and 100 to 1.0")]
        public void NormalizedValueDefaultMinMax()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Value = 0;
            Assert.AreEqual(0.0, d.NormalizedValue);
            d.Value = 100;
            Assert.AreEqual(1.0, d.NormalizedValue);
            d.Value = 50;
            Assert.AreEqual(0.5, d.NormalizedValue);
        }

        [TestMethod]
        [Description("Setting minimum and maximum to 150 and 300, 150 should normalise to 0 and 300 to 1")]
        public void NormalizedValuePosMinPosMax()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = 150;
            d.Maximum = 300;

            d.Value = 150;
            Assert.AreEqual(0.0, d.NormalizedValue);
            d.Value = 300;
            Assert.AreEqual(1.0, d.NormalizedValue);
            d.Value = 225;
            Assert.AreEqual(0.5, d.NormalizedValue);
        }

        [TestMethod]
        [Description("Setting minimum and maximum to -150 and 150, -150 should normalise to 0 and 150 to 1")]
        public void NormalizedValueNegMinPosMax()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -150;
            d.Maximum = 150;

            d.Value = -150;
            Assert.AreEqual(0.0, d.NormalizedValue);
            d.Value = 150;
            Assert.AreEqual(1.0, d.NormalizedValue);
            d.Value = 0;
            Assert.AreEqual(0.5, d.NormalizedValue);
        }

        [TestMethod]
        [Description("Setting minimum and maximum to -300 and -150, -300 should normalise to 0 and -150 to 1")]
        public void NormalizedValueNegMinNegMax()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -300;
            d.Maximum = -150;

            d.Value = -300;
            Assert.AreEqual(0.0, d.NormalizedValue);
            d.Value = -150;
            Assert.AreEqual(1.0, d.NormalizedValue);
            d.Value = -225;
            Assert.AreEqual(0.5, d.NormalizedValue);
        }

        [TestMethod]
        [Description("Setting minimum and maximum to -150 and -300 (ie swapped in error), -300 should normalise to 0 and -150 to 1")]
        public void NormalizedValueNegMinNegMaxSwappedMaxLessThanMin()
        {
            SimpleMockDashboard d = new SimpleMockDashboard();
            d.Minimum = -150;
            d.Maximum = -300;

            d.Value = -300;
            Assert.AreEqual(0.0, d.NormalizedValue);
            d.Value = -150;
            Assert.AreEqual(1.0, d.NormalizedValue);
            d.Value = -225;
            Assert.AreEqual(0.5, d.NormalizedValue);
        }



    }
}
