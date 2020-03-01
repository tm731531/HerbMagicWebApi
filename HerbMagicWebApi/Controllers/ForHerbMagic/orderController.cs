using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HerbMagicWebApi.Models.ObjectModels;

namespace HerbMagicWebApi.Controllers
{
    public class orderController : ApiController
    {
        [Route("api/v1/orders")]
        /// <summary>
        /// 获得订单清单
        /// </summary>
        /// <param name="doctor_id">订单的 doctor 的 id 号码</param>
        /// <remarks>
        /// OK
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<OrderObject>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Gets(string doctor_id)
        {
            if (doctor_id != "500" )
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<OrderObject>());
            }
            else
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }

        /// <summary>
        /// 获得该医师资讯
        /// </summary>
        /// <param name="order_id">订单的 id 号码</param>
        /// <remarks>
        /// 成功获得医师资讯
        /// </remarks>
        /// <response code="200">成功获取该订单资讯</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OrderObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(string order_id)
        {
            if (order_id != "500" && order_id != "404" && order_id != "400")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderObject());
            }
            else if (order_id == "400")
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "er_format")
                    ;

            }
            else if (order_id == "404")
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "er_noOrder");
            }
            else
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }

        /// <summary>
        /// 建立新订单
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// 成功获得医师资讯
        /// </remarks>
        /// <response code="200">成功获取该订单资讯</response>
        /// <response code="400">请求格式有误</response>
        /// <response code="404"></response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OrderPostObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Post([FromBody]OrderObject body)
        {
            if (body.wx_mch_id != "500" && body.wx_mch_id != "404" && body.wx_mch_id != "400")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderObject());
            }
            else if (body.wx_mch_id == "400")
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "er_format")
                    ;

            }
            else if (body.wx_mch_id == "404")
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "er_noOrder");
            }
            else
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }

        /// <summary>
        /// 修改该订单资讯
        /// </summary>
        /// <param name="order_id">订单的 id 号码</param>
        /// <param name="body"></param>
        /// <remarks>
        /// 更新成功
        /// </remarks>
        /// <response code="200">更新成功</response>
        /// <response code="400">请求格式有误</response>
        /// <response code="404">找不到该笔订单</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Put(string order_id, [FromBody]OrderObject body)
        {
            if (body.wx_mch_id != "500" && body.wx_mch_id != "404" && body.wx_mch_id != "400")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderObject());
            }
            else if (body.wx_mch_id == "400")
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "er_format")
                    ;

            }
            else if (body.wx_mch_id == "404")
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "er_noOrder");
            }
            else
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }

       
    }
}
