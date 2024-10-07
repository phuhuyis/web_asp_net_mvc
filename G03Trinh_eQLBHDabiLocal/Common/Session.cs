using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Common
{
    public class Session
    {
        public static object getSession(String sessionName)
        {
            if(HttpContext.Current.Session == null)
                return null;
            return HttpContext.Current.Session[sessionName];
        }

        public static void setSession(String sessionName, object obj)
        {
            HttpContext.Current.Session[sessionName] = obj;
        }

        public static void delSession(String sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}