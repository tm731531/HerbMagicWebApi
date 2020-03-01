using HerbMagicWebApi.Common;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HerbMagicWebApi.Models.ObjectModels;

namespace HerbMagicWebApi.Controllers.ForTom
{
    public class CEMSHourController : ApiController
    {


        public static string connectionString =
           ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
        public string TableName = "CEMSHour";

        [HttpGet]
        [Route("api/v1/CEMSHour")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get()
        {
            var url = "http://data.tycg.gov.tw/opendata/datalist/datasetMeta/download?id=0e97f604-83db-48b9-b3de-4aeaa8ce68c7&rid=d0b42df7-cd03-44b8-b8fb-c61893765db4";
            DBWriter(GetData(url));
            url = "https://data.tycg.gov.tw/opendata/datalist/datasetMeta/download?id=5a17deb8-eb08-45a0-98e9-199e53ea7a61&rid=28bc4efa-a1c3-4d2e-8705-2010237b82ed";
            DBWriter(GetData(url));

            var response = new object();
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
        public CEMSHour GetData(string url) {return HttpHelper.GetRequest<CEMSHour>(url); }
        public void DBWriter(CEMSHour data) {
            foreach (var d in data.records)
            {
                DapperHelper.InsertSQL<Record>(connectionString, TableName, d);
            }
        }
        public class Record
        {
            public int SeqNo { get; set; }
            public string abbr { get; set; }
            public string dpNo { get; set; }
            public string desp { get; set; }
            public string year { get; set; }
            public string month { get; set; }
            public string day { get; set; }
            public string time { get; set; }
            public float value { get; set; }
            public string status { get; set; }
            public string unit { get; set; }
            public string hashCode { get; set; }
        }
        public class CEMSHour
        {
            public List<Record> records { get; set; }
        }




    }
}
