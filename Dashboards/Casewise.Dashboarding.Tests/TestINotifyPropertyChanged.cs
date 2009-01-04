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
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Casewise.Dashboarding.Tests
{
    /// <summary>
    /// Very basic templated test for INotifyProperty changed events being raised
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestINotifyPropertyChanged<T>  where T : INotifyPropertyChanged
    {
        private string _property;
        private bool _passed;

        /// <summary>
        /// Test if the PropertyChanged event is raised. The action should set the property in
        /// the propertyName parameter, the class ensures that the PropertyChanged event is the correct
        /// one.
        /// </summary>
        /// <param name="item">an instance to test</param>
        /// <param name="action">an action to modify a property</param>
        /// <param name="propertyName">The property we are manipulating</param>
        public void AssertChange(T item, Action<T> action, string propertyName)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(VerifyChange);
            _property = propertyName;
            action(item);
            Assert.IsTrue(_passed);
        }

        /// <summary>
        /// If the expected property is in he event args mark us a passed. We have to assue that settin one
        /// property may lead to other property changes being raised (calculated props cause this issue)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        void VerifyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _property && !_passed)
            {
                _passed = true;
            }
        }


    }
}
