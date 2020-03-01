using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common
{
    public class Function
    {

        public static DateTime GetTime()
        {
            return DateTime.Now;

        }
        public static DateTime GetUTCTime()
        {
            return DateTime.UtcNow;
        }
    }
}