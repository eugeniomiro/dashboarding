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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeplex.Dashboarding;
using Microsoft.Silverlight.Testing;

namespace Casewise.Dashboarding.Tests
{
    [TestClass]
    public class BidirectionalDashboardTests: SilverlightTest
    {
        [TestMethod]
        [Description("We should get a property change notification when accessing via a property directly")]
        public void IsBidirectionalPropertyChangeNotification()
        {
            var tinp = new TestINotifyPropertyChanged<BidirectionalDashboard>();
            var action = new Action<BidirectionalDashboard>(p => p.IsBidirectional = !p.IsBidirectional);
            tinp.AssertChange(new SimpleMockBidirectionaldashboard(), action, "IsBidirectional");
        }

        [TestMethod]
        [Description("We should get a property change notification when accessing via a dependacy property and SetValue")]
        public void IsBidirectionalDependancyPropertyChangeNotification()
        {
            var tinp = new TestINotifyPropertyChanged<BidirectionalDashboard>();
            var action = new Action<BidirectionalDashboard>(p => p.SetValue(BidirectionalDashboard.IsBidirectionalProperty, !p.IsBidirectional));
            tinp.AssertChange(new SimpleMockBidirectionaldashboard(), action, "IsBidirectional");
        }

        [TestMethod]
        [Description("Setting the property should change the value")]
        public void IsBidirectionalProperty()
        {
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();

            d.SetValue(BidirectionalDashboard.IsBidirectionalProperty, true);
            Assert.IsTrue(d.IsBidirectional);

            d.SetValue(BidirectionalDashboard.IsBidirectionalProperty, false);
            Assert.IsFalse(d.IsBidirectional);

            d.IsBidirectional = true;
            Assert.IsTrue(d.IsBidirectional);

            d.IsBidirectional = false;
            Assert.IsFalse(d.IsBidirectional);

        }


        [TestMethod]
        [Description("We should get a call to animate if IsBiDirectional changes")]
        public void IsBidirectionalPropertyChangeCallsAnimate()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = !d.IsBidirectional;
            Assert.IsTrue(d.AnimateCalled);
        }


        [TestMethod]
        [Description("We should get a call to show grab handle on mouse in if we are bidirectional")]
        public void MouseInShowsGrabHandleWhenBidirectional()
        {
 
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.BidirectionalDashboard_MouseEnter(this, null);
            Assert.IsTrue(d.ShowGrabHandleCalled);
        }

        [TestMethod]
        [Description("We should get a call to Animate on mouse in if we are bidirectional")]
        public void MouseInAnimatesWhenBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.BidirectionalDashboard_MouseEnter(this, null);
            Assert.IsTrue(d.AnimateCalled);
        }


        [TestMethod]
        [Description("We should NOT get a call to show grab handle on mouse in if we are not bidirectional")]
        public void MouseInNotShowGrabHandleWhenNotBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.BidirectionalDashboard_MouseEnter(this, null);
            Assert.IsFalse(d.ShowGrabHandleCalled);
        }



        [TestMethod]
        [Description("We should NOT get a call to Animate on mouse in if we are not bidirectional")]
        public void MouseInNotAnimateWhenNotBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.BidirectionalDashboard_MouseEnter(this, null);
            Assert.IsFalse(d.AnimateCalled);
        }







        [TestMethod]
        [Description("We should get a call to hide grab handle on mouse out if we are bidirectional")]
        public void MouseOutHidesGrabHandleWhenBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.GrabHandle_MouseLeave(this, null);
            Assert.IsTrue(d.HideGrabHandleCalled);
        }

        [TestMethod]
        [Description("We should get a call to Animate on mouse out if we are bidirectional")]
        public void MouseOutAnimatesWhenBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.GrabHandle_MouseLeave(this, null);
            Assert.IsTrue(d.AnimateCalled);
        }


        [TestMethod]
        [Description("We should NOT get a call to hide grab handle on mouse out if we are not bidirectional")]
        public void MouseOutNotHideGrabHandleWhenNotBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.GrabHandle_MouseLeave(this, null);
            Assert.IsFalse(d.HideGrabHandleCalled);
        }



        [TestMethod]
        [Description("We should NOT get a call to Animate on mouse out if we are not bidirectional")]
        public void MouseOutNotAnimateWhenNotBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.GrabHandle_MouseLeave(this, null);
            Assert.IsFalse(d.AnimateCalled);
        }

        [TestMethod]
        [Description("We should get a show grab handle call on mouse down as the user may click and release on the same control and never generate an enter event")]
        public void MouseDownCallsShowGrabHandle()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.ButtonDown(new Point(0,0));
            Assert.IsTrue(d.ShowGrabHandleCalled);
        }


        [TestMethod]
        [Description("We should Only receive mouse move events when the mouse is down and the control is bidirectional")]
        public void MouseMoveNotCalledWhenNotBidirectional()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = false;
            d.ButtonDown(new Point(0, 0));
            d.MoveToPoint(new Point(10, 10));
            Assert.IsFalse(d.MoveCalled);
        }

        [TestMethod]
        [Description("We should Only receive mouse move events when the mouse is down and the control is bidirectional")]
        public void MouseMoveCalledWhenBidirectionalAndMouseIsDown()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            d.MoveToPoint(end);
            Assert.IsTrue(d.MoveCalled);
        }
        
        [TestMethod]
        [Description("Does mouse move pass in the correct data")]
        public void MouseMovePassesCorrectData()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            d.MoveToPoint(end);
            Assert.AreEqual(start, d.MoveStartPos);
            Assert.AreEqual(end, d.MoveEndPos);
        }

        [TestMethod]
        [Description("We should Only receive mouse move events when the mouse is down and the control is bidirectional")]
        public void MouseMoveNotCalledWhenBidirectionalAndMouseIsUp()
        {

            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.MoveToPoint(new Point(10, 10));
            Assert.IsFalse(d.MoveCalled);
        }

        [TestMethod]
        [Description("Does mouse move pass in the correct data")]
        public void MouseUpCallsAnimate()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            d.MoveToPoint(end);
            d.AnimateCalled = false;
            d.MouseUpAction();
            Assert.IsTrue(d.AnimateCalled);
        }

        [TestMethod]
        [Description("Does mouse move pass in the correct data")]
        public void MouseUpHidesDragHandle()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            d.MoveToPoint(end);
            d.HideGrabHandleCalled = false;
            d.MouseUpAction();
            Assert.IsTrue(d.HideGrabHandleCalled);
        }




        [TestMethod]
        [Description("Is the control no longer grabbed after mouse release?")]
        public void MouseUpMarksNotGrabbed()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            d.MoveToPoint(end);
            d.MouseUpAction();
            Assert.IsFalse(d.Grabbed);
        }

        [TestMethod]
        [Description("Is the control grabbed after mouse down?")]
        public void MouseDownMarksAsGrabbed()
        {
            Point start = new Point(0, 0);
            Point end = new Point(10, 10);
            SimpleMockBidirectionaldashboard d = new SimpleMockBidirectionaldashboard();
            d.IsBidirectional = true;
            d.ButtonDown(start);
            Assert.IsTrue(d.Grabbed);
        }

    }
}
