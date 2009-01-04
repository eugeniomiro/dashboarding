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
    public class ColorPointTests
    {
        [TestMethod]
        public void SetHiColorProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.HiColor = Colors.Blue;
            Assert.AreEqual(cp.HiColor, Colors.Blue);
        }

        [TestMethod]
        public void SetHiColorDedpendancyProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.SetValue(ColorPoint.HiColorProperty, Colors.Blue);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
        }

        [TestMethod]
        public void SetLowColorProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.LowColor = Colors.Blue;
            Assert.AreEqual(cp.LowColor, Colors.Blue);
        }

        [TestMethod]
        public void SetLowColorDedpendancyProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.SetValue(ColorPoint.LowColorProperty, Colors.Blue);
            Assert.AreEqual(cp.LowColor, Colors.Blue);
        }

        [TestMethod]
        public void SetValueColorProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.Value = 22;
            Assert.AreEqual(22, cp.Value);
        }

        [TestMethod]
        public void SetValueColorDedpendancyProperty()
        {
            ColorPoint cp = new ColorPoint();
            cp.SetValue(ColorPoint.ValueProperty, 34.0);
            Assert.AreEqual(cp.Value, 34.0);
        }






        [TestMethod]
        public void SetHiColorPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("HiColor" == e.PropertyName);
                }
            };
            cp.HiColor = Colors.Blue;

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void SetHiColorDedpendancyPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("HiColor" == e.PropertyName);
                }
            };
            cp.SetValue(ColorPoint.HiColorProperty, Colors.Blue);
            Assert.AreEqual(cp.HiColor, Colors.Blue);

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void SetLowColorPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("LowColor" == e.PropertyName);
                }
            };
            cp.LowColor = Colors.Blue;
            Assert.AreEqual(cp.LowColor, Colors.Blue);

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void SetLowColorDedpendancyPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("LowColor" == e.PropertyName);
                }
            };
            cp.SetValue(ColorPoint.LowColorProperty, Colors.Blue);
            Assert.AreEqual(cp.LowColor, Colors.Blue);

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void SetValueColorPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("Value" == e.PropertyName);
                }
            };
            cp.Value = 22;
            Assert.AreEqual(22, cp.Value);

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void SetValueColorDedpendancyPropertyChanged()
        {
            bool passed = false;
            ColorPoint cp = new ColorPoint();
            
            cp.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (!passed)
                {
                    passed = ("Value" == e.PropertyName);
                }
            };

            cp.SetValue(ColorPoint.ValueProperty, 34.0);
            Assert.AreEqual(cp.Value, 34.0);

            Assert.IsTrue(passed);
        }



    }
}
