using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Diagnostics;

namespace Dashboard.WebServices
{

    /// <summary>
    /// Summary description for DashboardWebservice
    /// </summary>
    [WebService(Namespace = "http://www.CodePlex.com/Dashboarding/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class DashboardWebservice : System.Web.Services.WebService
    {
        private static Random rand = new Random();
        private static PerformanceCounter myCounter;
        static DashboardWebservice()
        {
             myCounter = new PerformanceCounter();
            myCounter.CategoryName = "Processor";
            myCounter.CounterName = "% Processor Time";
            myCounter.InstanceName = "_Total";

        }

        [WebMethod]
        [ScriptMethod]
        public int GetDashboardValueNoParams()
        {
            return rand.Next(100);
        }

        [WebMethod]
        [ScriptMethod]
        public int GetDashboardValueSingleIntegerAsRange(int range)
        {
            
            return rand.Next(range);
        }

        [WebMethod]
        [ScriptMethod]
        public bool GetDashboardValueBool()
        {

            return (rand.Next(1024) & 1) == 1;
        }

        [WebMethod]
        [ScriptMethod]
        public int GetCpuLoad()
        {
            return (int)myCounter.NextValue();


        }

        [WebMethod]
        [ScriptMethod]
        public int GetSawTooth()
        {

            return DateTime.Now.Second;

        }

        [WebMethod]
        [ScriptMethod]
        public int GetSquareWave()
        {
           
            if (DateTime.Now.Second < 15)
            {
                return 0;
            }
            if (DateTime.Now.Second < 30)
            {
                return 50;
            }
            if (DateTime.Now.Second < 45)
            {
                return 100;
            }
            return 50;

        }

    }
}
