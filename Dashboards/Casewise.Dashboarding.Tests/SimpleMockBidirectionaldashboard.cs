﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Codeplex.Dashboarding;

namespace Casewise.Dashboarding.Tests
{
    /// <summary>
    ///  a mock bidirectional dashboard control
    /// </summary>
    public class SimpleMockBidirectionaldashboard : BidirectionalDashboard
    {

        /// <summary>
        /// Has animate been called?
        /// </summary>
        public bool AnimateCalled { get; set; }

        /// <summary>
        /// The framework has invoked ShowGrabHandle
        /// </summary>
        public bool ShowGrabHandleCalled { get; set; }

        /// <summary>
        /// The framework has invoked HideGrabHandle
        /// </summary>
        public bool HideGrabHandleCalled { get; set; }

        /// <summary>
        /// The framework has invoked HideGrabHandle
        /// </summary>
        public bool MoveCalled { get; set; }

        /// <summary>
        /// Start point of the mouse move
        /// </summary>
        public Point MoveStartPos { get; set; }

        /// <summary>
        /// End point of the mouse move
        /// </summary>
        public Point MoveEndPos { get; set; }

        /// <summary>
        /// Are we grabbed
        /// </summary>
        public bool Grabbed { get { return IsGrabbed; } }

        /// <summary>
        /// if invoked mark the instance as having animated
        /// </summary>
        protected override void Animate()
        {
            AnimateCalled = true;
        }


        /// <summary>
        /// Highlight the grab handle as the mouse is in
        /// </summary>
        protected override void ShowGrabHandle()
        {
            ShowGrabHandleCalled = true;
        }

        /// <summary>
        /// Stop the highlight of the grab handle the mouse is out
        /// </summary>
        protected override void HideGrabHandle()
        {
            HideGrabHandleCalled = true;
        }


        /// <summary>
        /// Mouse is moving, move the diagram
        /// </summary>
        /// <param name="mouseDownPosition">origin of the drag</param>
        /// <param name="currentPosition">where the mouse is now</param>
        protected override void OnMouseGrabHandleMove(Point mouseDownPosition, Point currentPosition)
        {
            MoveCalled = true;
            MoveStartPos = mouseDownPosition;
            MoveEndPos = currentPosition;
        }



    }
}