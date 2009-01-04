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
using Codeplex.Dashboarding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class ColorPointCollectionTests
    {
        [TestMethod]
        public void EmptyCollectionRetrunsNull()
        {
            ColorPointCollection cps = new ColorPointCollection();
            Assert.IsNull(cps.GetColor(0.3));
        }


        [TestMethod]
        public void OnePointGivesValuesBelow()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 }
            };
            ColorPoint cp = cps.GetColor(-10);
            Assert.IsNotNull(cp);
        }

        [TestMethod]
        public void OnePointGivesValuesAbove()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 }
            };
            ColorPoint cp = cps.GetColor(1000);
            Assert.IsNotNull(cp);
        }

        [TestMethod]
        public void OnePointGivesValuesEqual()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 }
            };
            ColorPoint cp = cps.GetColor(10);
            Assert.IsNotNull(cp);
        }

        [TestMethod]
        public void TwoPointsGivesLowerBoundBelowRange()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 }
            };
            ColorPoint cp = cps.GetColor(-10);
            Assert.IsNotNull(cp);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
        }

        [TestMethod]
        public void TwoPointsGivesUperBoundAboveRange()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 }
            };
            ColorPoint cp = cps.GetColor(30);
            Assert.IsNotNull(cp);
            Assert.AreEqual(cp.HiColor, Colors.Red);
        }

        [TestMethod]
        public void TwoPointsGivesLowerBoundEqual()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 }
            };
            ColorPoint cp = cps.GetColor(10);
            Assert.IsNotNull(cp);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
        }

        [TestMethod]
        public void TwoPointsGivesUperBoundEqual()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 }
            };
            ColorPoint cp = cps.GetColor(20);
            Assert.IsNotNull(cp);
            Assert.AreEqual(cp.HiColor, Colors.Red);
        }

        [TestMethod]
        public void TwoPointsGivesCorrectColor()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 }
            };
            ColorPoint cp = cps.GetColor(13);
            Assert.IsNotNull(cp);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
        }


        [TestMethod]
        public void MultiColorsAreCorrect()
        {
            ColorPointCollection cps = new ColorPointCollection
            {
                new ColorPoint { HiColor = Colors.Blue, LowColor = Colors.Blue, Value = 10 },
                new ColorPoint { HiColor = Colors.Red, LowColor = Colors.Red, Value = 20 },
                new ColorPoint { HiColor = Colors.Yellow, LowColor = Colors.Yellow, Value = 30 },
            };

            ColorPoint cp = cps.GetColor(-10);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
            cp = cps.GetColor(10);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
            cp = cps.GetColor(19.99);
            Assert.AreEqual(cp.HiColor, Colors.Blue);
            cp = cps.GetColor(20);
            Assert.AreEqual(cp.HiColor, Colors.Red);
            cp = cps.GetColor(25);
            Assert.AreEqual(cp.HiColor, Colors.Red);
            cp = cps.GetColor(29.9999);
            Assert.AreEqual(cp.HiColor, Colors.Red);
            cp = cps.GetColor(30);
            Assert.AreEqual(cp.HiColor, Colors.Yellow);
            cp = cps.GetColor(31);
            Assert.AreEqual(cp.HiColor, Colors.Yellow);




        }



    }
}
