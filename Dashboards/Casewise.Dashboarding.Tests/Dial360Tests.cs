﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeplex.Dashboarding;
using Microsoft.Silverlight.Testing;
using System.Windows.Media;
using System.Windows;

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class Dial360Tests : SilverlightTest
    {
        private Dial360 _meter;

        [TestInitialize]
        public void Prepare()
        {
            _meter = new Dial360();
            this.TestPanel.Children.Add(_meter);
        }


        [TestMethod]
        public void TestTextColor()
        {
            var tinp = new TestINotifyPropertyChanged<Dial360>();
            var action = new Action<Dial360>(p => p.TextColor = Colors.Yellow);
            tinp.AssertChange(_meter, action, "TextColor");
            action = new Action<Dial360>(p => p.SetValue(Dial360.TextColorProperty, Colors.Purple));
            tinp.AssertChange(_meter, action, "TextColor");
            Assert.AreEqual(_meter.TextColor, Colors.Purple);
        }

        [TestMethod]
        public void TestTextVisibility()
        {
            var tinp = new TestINotifyPropertyChanged<Dial360>();
            var action = new Action<Dial360>(p => p.TextVisibility = (p.TextVisibility == Visibility.Collapsed) ? Visibility.Visible: Visibility.Collapsed );
            tinp.AssertChange(_meter, action, "TextVisibility");
            Visibility nv = (_meter.TextVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            action = new Action<Dial360>(p => p.SetValue(Dial360.TextVisibilityProperty, nv));
            tinp.AssertChange(_meter, action, "TextVisibility");
            Assert.AreEqual(_meter.TextVisibility, nv);
        }


        [TestMethod]
        public void TestFaceColorRange()
        {
            ColorPointCollection cp = new ColorPointCollection();
            var tinp = new TestINotifyPropertyChanged<Dial360>();
            var action = new Action<Dial360>(p => p.FaceColorRange = cp);
            tinp.AssertChange(_meter, action, "FaceColorRange");
            action = new Action<Dial360>(p => p.SetValue(Dial360.FaceColorRangeProperty, cp));
            tinp.AssertChange(_meter, action, "FaceColorRange");
            Assert.AreSame(_meter.FaceColorRange, cp);
        }

        [TestMethod]
        public void TestNeedleColorRange()
        {
            ColorPointCollection cp = new ColorPointCollection();
            var tinp = new TestINotifyPropertyChanged<Dial360>();
            var action = new Action<Dial360>(p => p.NeedleColorRange = cp);
            tinp.AssertChange(_meter, action, "NeedleColorRange");
            action = new Action<Dial360>(p => p.SetValue(Dial360.NeedleColorRangeProperty, cp));
            tinp.AssertChange(_meter, action, "NeedleColorRange");
            Assert.AreSame(_meter.NeedleColorRange, cp);
        }


    }
}