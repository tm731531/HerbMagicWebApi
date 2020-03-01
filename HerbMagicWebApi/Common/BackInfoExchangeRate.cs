using System;

namespace HerbMagicWebApi.Common
{
    internal class BackInfoExchangeRate
    {
        public string currency { get; set; }
        public string buy { get; set; }
        public string sell { get; set; }
        public DateTime InfoDay { get; set; }
    }
}