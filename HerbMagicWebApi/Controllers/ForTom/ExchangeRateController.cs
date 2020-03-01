using HerbMagicWebApi.Common;
using HerbMagicWebApi.Models;
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
    /// <summary>
    /// 匯率
    /// </summary>
   
    /// <remarks>匯率</remarks>
    public class ExchangeRateController : ApiController
    {
        /// <summary>
        /// 獨立案件 資料庫拉開
        /// </summary> 
        public static string connectionString =
            ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        /// <summary>
        /// 對應資料庫的表格
        /// </summary>
        public string TableName = "ExchangeRate";


        /// <summary>
        /// 列表匯率
        /// </summary>
        /// <remarks>這一列方法都是查詢列表</remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Server error</response>
        /// <returns>匯率的基本訊息</returns>
        [HttpGet]
        [Route("api/v1/ExchangeRate")]

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MainExchangeRate>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get()
        {
            var response = DapperHelper.Search<MainExchangeRate>(connectionString, "select * from " + TableName);
            response= response.OrderBy(x => x.Currency).ThenBy(x => x.SpotExchangeRateSells);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        /// <summary>
        /// 列表匯率 單一seqno
        /// </summary>
        /// <remarks>這一列方法都是單一查詢</remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Server error</response>

        /// <returns>查詢特殊關鍵字</returns>
        [Route("api/v1/ExchangeRateSearch")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainExchangeRate))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(string keyword)
        {
            var response = DapperHelper.Search<MainExchangeRate>(connectionString, "select * from " + TableName + " where Currency like N'%" + keyword + "%'");
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        /// <summary>
        /// 增加匯率
        /// </summary>
        /// <remarks>這一列方法都是單一增加</remarks>
        /// <param name="value">修改的資料</param>
        /// <response code="200">OK</response>
        /// <response code="500">Server error</response>

        /// <returns>增加資料</returns>
        [HttpPost]
        [Route("api/v1/ExchangeRate")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainExchangeRate))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Post()
        {
            try
            {
                foreach (var q in Common.Data.BankRequestList)
                {

                    foreach (var q2 in HttpHelper.GetRequest<ExchangeRateModels>(q.Url).ResultSet.Result)
                    {
                        MainExchangeRate mer = new MainExchangeRate();
                        mer.BankCode = q2.V1;
                        mer.Bank = q2.V2;
                        mer.Currency = q.Currey;
                        DateTime dt = new DateTime();
                        double v4 = default(double);
                        double v5 = default(double);
                        double v6 = default(double);
                        double v7 = default(double);
                        DateTime.TryParse(q2.V3, out dt);
                        Double.TryParse(q2.V4, out v4);
                        Double.TryParse(q2.V5, out v5);
                        Double.TryParse(q2.V6, out v6);
                        Double.TryParse(q2.V7, out v7);
                        mer.Date = dt;
                        mer.SpotExchangeRateBuys = v4;
                        mer.SpotExchangeRateSells = v5;
                        mer.CashExchangeRateBuys = v6;
                        mer.CashExchangeRateSells = v7;
                        DapperHelper.InsertSQL<MainExchangeRate>
                           (connectionString, TableName, mer);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 修改匯率資料
        /// </summary>
        /// <param name="value">修改的資料</param>
        /// <remarks>這一列方法都是單一修改</remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Server error</response>

        /// <returns>修改資料</returns>
        [HttpPut]
        [Route("api/v1/ExchangeRate")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainExchangeRate))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Put([FromBody]MainExchangeRate value)
        {
            var response = DapperHelper.UpdateSQL<MainExchangeRate>(connectionString, TableName, value);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
        /// <summary>
        /// 刪除匯率
        /// </summary>
        /// <param name="value">修改的資料</param>
        /// <remarks>這一列方法都是單一修改</remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Server error</response>

        /// <returns>修改資料</returns>
        [HttpDelete]
        [Route("api/v1/ExchangeRate")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MainExchangeRate))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]

        public HttpResponseMessage Delete([FromBody]MainExchangeRate value)
        {
            var response = DapperHelper.DeleteSQL(connectionString, TableName, value.SeqNo);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
    }
}
