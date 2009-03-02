﻿#region Copyright 2008 David Black

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

using Codeplex.Dashboarding;
using System.Windows.Controls;

namespace Casewise.Dashboarding.Tests
{
    public class SimpleMockDashboard : Dashboard
    {
        
        /// <summary>
        /// Has the animate method been called?
        /// </summary>
        public bool AnimateCalled { get; set; }


        protected override void Animate()
        {
            AnimateCalled = true;
        }

        /// <summary>
        /// Gets the resource root. This allow us to access the Storyboards in a Silverlight/WPf
        /// neutral manner
        /// </summary>
        /// <value>The resource root.</value>
        protected override Grid ResourceRoot
        {
            get { return null; }
        }

        protected override void UpdateTextColor()
        {
        }

        protected override void UpdateTextVisibility()
        {
        }

        /// <summary>
        /// The format string for the value has changed
        /// </summary>
        protected override void UpdateTextFormat()
        {
        }
    }
}
