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
        
        /// <remarks/>
        public bool GetDashboardValueBool() {
            object[] results = this.Invoke("GetDashboardValueBool", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(bool));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDashboardValueBool(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDashboardValueBool", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(bool), callback, asyncState);
        }
        
        /// <remarks/>
        public bool EndGetDashboardValueBool(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public int GetCpuLoad() {
            object[] results = this.Invoke("GetCpuLoad", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetCpuLoad(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetCpuLoad", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int), callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetCpuLoad(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public int GetSawTooth() {
            object[] results = this.Invoke("GetSawTooth", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSawTooth(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSawTooth", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int), callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetSawTooth(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public int GetSquareWave() {
            object[] results = this.Invoke("GetSquareWave", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSquareWave(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSquareWave", new System.Windows.Browser.Net.SoapHttpClientProtocol.ServiceParameter[0], typeof(int), callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetSquareWave(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
    }
}

#pragma warning restore 1591