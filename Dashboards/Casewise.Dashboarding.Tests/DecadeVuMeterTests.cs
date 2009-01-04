using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeplex.Dashboarding;
using Microsoft.Silverlight.Testing;
using System.Windows.Media;

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class DecadeVuMeterTests: SilverlightTest
    {
        private DecadeVuMeter _meter;

        [TestInitialize]
        public void Prepare()
        {
            _meter = new DecadeVuMeter();
            this.TestPanel.Children.Add(_meter);
        }

        [TestMethod]
        public void TestBorderColorProperty()
        {
            var tinp = new TestINotifyPropertyChanged<DecadeVuMeter>();
            var action = new Action<DecadeVuMeter>(p => p.BorderColor = Colors.Yellow);
            tinp.AssertChange(_meter, action, "BorderColor");
            action = new Action<DecadeVuMeter>(p => p.SetValue(DecadeVuMeter.BorderColorProperty,  Colors.Purple));
            tinp.AssertChange(_meter, action, "BorderColor");
            Assert.AreEqual(_meter.BorderColor, Colors.Purple);
        }

        [TestMethod]
        public void TestLedOnColorProperty()
        {
            var tinp = new TestINotifyPropertyChanged<DecadeVuMeter>();
            var action = new Action<DecadeVuMeter>(p => p.LedOnColor = Colors.Yellow);
            tinp.AssertChange(_meter, action, "LedOnColor");
            action = new Action<DecadeVuMeter>(p => p.SetValue(DecadeVuMeter.LedOnColorProperty, Colors.Purple));
            tinp.AssertChange(_meter, action, "LedOnColor");
            Assert.AreEqual(_meter.LedOnColor, Colors.Purple);
        }


        [TestMethod]
        public void TestLedOffColorProperty()
        {
            var tinp = new TestINotifyPropertyChanged<DecadeVuMeter>();
            var action = new Action<DecadeVuMeter>(p => p.LedOffColor = Colors.Yellow);
            tinp.AssertChange(_meter, action, "LedOffColor");
            action = new Action<DecadeVuMeter>(p => p.SetValue(DecadeVuMeter.LedOffColorProperty, Colors.Purple));
            tinp.AssertChange(_meter, action, "LedOffColor");
            Assert.AreEqual(_meter.LedOffColor, Colors.Purple);
        }

    
    }
}