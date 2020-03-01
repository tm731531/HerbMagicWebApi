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


namespace HerbMagicWebApi.Controllers.ForTom
{
    /// <summary>
    /// 桌游區
    /// </summary>
    public class BoardGameController : ApiController
    {
        /// <summary>
        /// 獨立案件 資料庫拉開
        /// </summary>
        public static string connectionString =
            ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        /// <summary>
        /// 對應資料庫的表格
        /// </summary>
        public string TableName = "BoardGame";


        /// <summary>
        /// 列表桌遊
        /// </summary>
        /// <returns>桌遊的基本訊息</returns>
        [HttpGet]
        [Route("api/v1/BoardGame")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MainBoardGame>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get()
        {
            var response = DapperHelper.Search<MainBoardGame>(
                connectionString,
                "select * from " + TableName);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// 列表桌遊 單一seqno
        /// </summary>
        /// <returns></returns>
        [Route("api/v1/BoardGameSearch")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainBoardGame))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(string keyword)
        {
            var response = DapperHelper.Search<MainBoardGame>(connectionString, "select * from " + TableName + " where BookName like N'%" + keyword + "%'");
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        /// <summary>
        /// 增加桌遊
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/BoardGame")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainBoardGame))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Post([FromBody]MainBoardGame value)
        {
            if (value.Date == DateTime.MinValue) { value.Date = Function.GetTime(); };
            var response = (int)DapperHelper.InsertSQL<MainBoardGame>(connectionString, TableName, value);
            value.SeqNo = response;
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        /// <summary>
        /// 修改桌遊資料
        /// </summary>
        /// <param name="value">修改的資料</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/BoardGame")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainBoardGame))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Put([FromBody]MainBoardGame value)
        {
            var response = DapperHelper.UpdateSQL<MainBoardGame>(connectionString, TableName, value);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        /// <summary>
        /// 刪除桌遊
        /// </summary>
        [HttpDelete]
        [Route("api/v1/BoardGame")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainBoardGame))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]

        public HttpResponseMessage Delete([FromBody]MainBoardGame value)
        {
            var response = DapperHelper.DeleteSQL(connectionString, TableName, value.SeqNo);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }


    }

}
