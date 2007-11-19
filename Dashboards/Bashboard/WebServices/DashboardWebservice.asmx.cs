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

        [WebMethod]
        [ScriptMethod]
        public int GetDashboardValueNoParams()
        {
            Random r = new Random();
            return r.Next(100);
        }

        [WebMethod]
        [ScriptMethod]
        public int GetDashboardValueSingleIntegerAsRange(int range)
        {
            Random r = new Random();
            return r.Next(range);
        }
    }
}
