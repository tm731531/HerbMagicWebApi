using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common
{
    public class TempCache
    {
        public string userId { get; set; }
        public string groupId { get; set; }
        public string messageText { get; set; }
        public DateTime timeStamp { get; set; }
    }
}