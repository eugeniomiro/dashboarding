﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.1378.
// 
#pragma warning disable 1591

namespace David.Black.Bashboarding.dashboardRef {
    using System;
    
    
    /// <remarks/>
    public partial class DashboardWebservice : System.Windows.Browser.Net.SoapHttpClientProtocol {
        
        /// <remarks/>
        public DashboardWebservice() {
            this.Url = "http://localhost:52114/WebServices/DashboardWebservice.asmx";
        }
        
        /// <remarks/>
        public int GetDashboardValueNoParams() {
            object[] results = this.Invoke("GetDashboardValueNoParams", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDashboardValueNoParams(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDashboardValueNoParams", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int), callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetDashboardValueNoParams(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public int GetDashboardValueSingleIntegerAsRange(int range) {
            object[] results = this.Invoke("GetDashboardValueSingleIntegerAsRange", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[] {
                        new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter("range", range)}, typeof(int));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDashboardValueSingleIntegerAsRange(int range, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDashboardValueSingleIntegerAsRange", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[] {
                        new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter("range", range)}, typeof(int), callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetDashboardValueSingleIntegerAsRange(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
    }
}

#pragma warning restore 1591