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

namespace Codeplex.Dashboarding
{
    /// <summary>
    /// We target Silverlight and WPF, this level of the class hierarchy 
    /// deals with the differences by providing helper methods
    /// </summary>
    public abstract class PlatformIndependentDashboard : Dashboard
    {
        /// <summary>
        /// Gets a story board by name. WPF storyboards are stored as Resources and are accessed
        /// by x:Key with in the resources of the layoutRoot, silverlight prvides references, by
        /// we do a findName on the layoutRoot to be consistent
        /// </summary>
        /// <param name="layoutRoot">The layout root.</param>
        /// <param name="name">The name of the Storyboard.</param>
        /// <returns>The desired Storyboard</returns>
        protected Storyboard GetStoryBoard(string name)
        {
#if WPF
            return (Storyboard)ResourceRoot.Resources[name];
#else
            return (Storyboard)ResourceRoot.FindName(name);
#endif        
        }


        /// <summary>
        /// Gets the StoryBoard used to animate the main indicator on a gauge
        /// for example the needle for a Dial360
        /// </summary>
        /// <value>The animate position.</value>
        protected Storyboard AnimateIndicatorStoryboard
        {
            get { return GetStoryBoard("_swipe"); }
        }

        /// <summary>
        /// Gets the StoryBoard used to animate the grab handle for a bidirectional controle
        /// for example the two triangles on a Dial360. This is here rather than on BidirectionalDashboard
        /// for ease of implementation and to allow us to encapsulate all WPF bridging code in one class
        /// </summary>
        /// <value>The animate position.</value>
        protected Storyboard AnimateGrabHandleStoryboard
        {
            get { return GetStoryBoard("_moveGrab"); }
        }


        /// <summary>
        /// The common pattern in our classes is that we have a storyboard
        /// with a single SplineDoubleKeyFrame as a child and that has a single 
        /// terminal SplineDoubleKeyFrame setting the deflection of the animation.
        /// <para>
        /// This method does this through inspection rather than using a reference to the key frame
        /// because wpf does not generate the story board and sub components as references
        /// </para>
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="point">The point.</param>
        protected void SetFirstChildSplineDoubleKeyFrameTime(Storyboard sb, double point)
        {
            if (sb.Children.Count != 1)
                return;
            DoubleAnimationUsingKeyFrames anim = sb.Children[0] as DoubleAnimationUsingKeyFrames;
            if (anim == null || anim.KeyFrames.Count != 1)
                return;
            SplineDoubleKeyFrame sdf = anim.KeyFrames[0] as SplineDoubleKeyFrame;
            if (sdf != null)
            {
                sdf.Value = point;
            }
        }

        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected abstract Grid ResourceRoot { get; }
    }
}
