using HerbMagicWebApi.Common;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HerbMagicWebApi.Models.BookStoreModels;
using static HerbMagicWebApi.Models.ObjectModels;

namespace HerbMagicWebApi.Controllers
{
    public class HistoryController : ApiController
    {
        /// <summary>
        /// 獨立案件 資料庫拉開
        /// </summary>
        public static string connectionString =
            ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        /// <summary>
        /// 對應資料庫的表格
        /// </summary>
        public string TableName = "History";


        /// <summary>
        /// 列表書名
        /// </summary>
        /// <returns>書的基本訊息</returns>
        [HttpGet]
        [Route("api/v1/History")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MainHistory>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get()
        {
            var response = DapperHelper.Search<MainHistory>(ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString, "select * from " + TableName);

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        // GET api/<controller>/5
        /// <summary>
        /// 列表書名 單一seqno
        /// </summary>
        /// <returns></returns>
        [Route("api/v1/HistorySearch")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainHistory))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(string keyword)
        {
            var response = DapperHelper.Search<MainHistory>(connectionString, "select * from " + TableName + " where Description like N'%" + keyword + "%'");
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        /// <summary>
        /// 增加書
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/History")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainHistory))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Post([FromBody]MainHistory value)
        {
            if (value.Date == DateTime.MinValue) { value.Date = Function.GetTime(); };

            var response = (int)DapperHelper.InsertSQL<MainHistory>(connectionString, TableName, value);
            value.SeqNo = response;
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        /// <summary>
        /// 修改書資料
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value">修改的資料</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/History")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainHistory))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Put([FromBody]MainHistory value)
        {
            var response = DapperHelper.UpdateSQL<MainHistory>(connectionString, TableName, value);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
        /// <summary>
        /// 刪除書
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("api/v1/History")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainHistory))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]

        public HttpResponseMessage Delete([FromBody]MainHistory value)
        {
            var response = DapperHelper.DeleteSQL(connectionString, TableName, value.SeqNo);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
    }
}
