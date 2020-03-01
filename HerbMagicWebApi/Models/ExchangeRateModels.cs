using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class ExchangeRateModels
    {
        public ResultSet ResultSet { get; set; }
    }
    public class Result
    {
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; }
        public string V6 { get; set; }
        public string V7 { get; set; }
    }

    public class ResultSet
    {
        public string TAGID { get; set; }
        public int StatusCode { get; set; }
        public string Comment { get; set; }
        public string ExpireTime { get; set; }
        public int DataLength { get; set; }
        public int FieldLength { get; set; }
        public List<Result> Result { get; set; }
    }
}