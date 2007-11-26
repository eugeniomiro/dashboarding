using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace CodePlex.Dashboarding.Silverlight.Services
{
    public class WebServiceManager
    {
        private static Dictionary<string, WebServiceDefinition> _registeredMethods = new Dictionary<string, WebServiceDefinition>();
        public static void Register(WebServiceDefinition def)
        {
            def.Validate();
            if (!_registeredMethods.ContainsKey(def.LocalIdentifier))
            {
                _registeredMethods.Add(def.LocalIdentifier, def);
            }
        }

        public static WebServiceDefinition Retrieve(string localIdentifier)
        {
            return _registeredMethods[localIdentifier];
        }

        public static void Unregister(string localIdentifier)
        {
            if (_registeredMethods.ContainsKey(localIdentifier))
            {
                _registeredMethods.Remove(localIdentifier);
            }
        }
    }
}
