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
using System.Reflection;

namespace CodePlex.Dashboarding.Silverlight.Services
{
    public class WebServiceDefinition
    {
        private MethodInfo _targetMethod;
        private MethodInfo _BeginMethod;
        private MethodInfo _EndMethod;

        private bool validated;

        public string LocalIdentifier { get; set; }
        public Type Class { get; set; }
        public string MethodName { get; set; }
        public int RefreshPeriod { get; set; }
        public WebServiceParameterCollection Parameters { get; set; }


        public WebServiceDefinition()
        {
        }

        public object BeginAsyncCall(AsyncCallback callback)
        {
            Validate();
            var args = new List<object>();
            args.AddRange(Parameters.AsArgumentArray);
            args.Add(callback);
            args.Add(null);
            Object o = Activator.CreateInstance(Class);

            _BeginMethod.Invoke(o, args.ToArray());

           
            return o;
        }

        public int EndAsyncCall(IAsyncResult res, object ws)
        {
            Validate();
            object ret = _EndMethod.Invoke(ws, new object [] {res});
            return (int)ret;
        }


        internal void Validate()
        {
            if (!validated)
            {
                CheckMethodExists();
                CheckReturnValueOfFunction();
                CheckParameters();
            }
        }

        private void CheckReturnValueOfFunction()
        {
            Type returnValue = _targetMethod.ReturnType;
            if (!typeof(Int32).IsAssignableFrom(returnValue))
            {
                throw new DashboardWebServiceException("Web service class '" + Class.Name + "' method '" + MethodName + "' the return value of the method '"+returnValue.Name+ " is not assignable to the Value of the guage or dial (int)");
            }
        }

        private void CheckParameters()
        {
            ParameterInfo [] parameterInfos = _targetMethod.GetParameters();

            if (parameterInfos.Length != Parameters.Count)
            {
                throw new DashboardWebServiceException("Web service class '" + Class.Name + " 'method '"+MethodName +"' expected" + Parameters.Count + " parameters but found " + parameterInfos.Length);
            }

            var nameToParam = new Dictionary<string, ParameterInfo>();            
            foreach (ParameterInfo info in parameterInfos)
            {
                nameToParam.Add(info.Name, info);
            }

            foreach (WebServiceParameter wsp in Parameters)
            {
                if (!nameToParam.ContainsKey(wsp.Name))
                {
                    throw new DashboardWebServiceException("Web service class '" + Class.Name + "' method '" + MethodName + "' parameter '" + wsp.Name + "' not found in reflected class method. Check the spelling in your WebServiceParameter initialization." );
                }
                if (!nameToParam[wsp.Name].ParameterType.IsAssignableFrom(wsp.Value.GetType()))
                {
                    throw new DashboardWebServiceException("Web service class '" + Class.Name + "' method '" + MethodName + "' parameter '" + wsp.Name + "' the value specified is not assignable to the prameter of the method." + " Your value is of type '" + wsp.Value.GetType().Name + "' but the target classes paramter is of type '" + nameToParam[wsp.Name].ParameterType.Name+"'");
                }
            }

            for (int i = 0; i<parameterInfos.Length; i++)
            {
                if (Parameters[i].Name != parameterInfos[i].Name)
                {
                    throw new DashboardWebServiceException("Web service class '" + Class.Name + "' method '" + MethodName + "' parameter is in the wrong place, expected '" + parameterInfos[i].Name + "' but got '" + Parameters[i].Name + "'");
                }
            }

        }

        private void CheckMethodExists()
        {
            bool hasMethod = false;
            bool hasBeginMethod = false;
            bool hasEndMethod = false;

            MethodInfo[] methods = Class.GetMethods();

            foreach (MethodInfo info in methods)
            {
                if (info.Name == MethodName)
                {
                    _targetMethod = info;
                    hasMethod = true;
                }

                if (info.Name == "Begin" + MethodName)
                {
                    _BeginMethod = info;
                    hasBeginMethod = true;
                }

                if (info.Name == "End" + MethodName)
                {
                    _EndMethod = info;
                    hasEndMethod = true;
                }
            }

            if (!hasMethod)
            {
                throw new DashboardWebServiceException("Web service class '" + Class.Name + "' does not support the specified method '" + MethodName+"'");
            }

            if (!hasBeginMethod)
            {
                throw new DashboardWebServiceException("Web service class '" + Class.Name + "' does not support the asynchronous begin method '" + "Begin" + MethodName + "'");
            }

            if (!hasEndMethod)
            {
                throw new DashboardWebServiceException("Web service class '" + Class.Name + "' does not support the asynchronous end method '" + "End" + MethodName + "'");
            }
        }
    }
}
