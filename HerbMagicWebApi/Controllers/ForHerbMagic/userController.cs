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
    public class userController : ApiController
    {


        /// <summary>
        /// 获得该使用者资讯
        /// </summary>
        /// <param name="id">user_phone(该使用者手机号码)</param>
        /// <remarks>
        /// 成功获得用户资讯
        /// </remarks>
        /// <response code="200">成功获得用户资讯</response>
        /// <response code="404">查无此用户</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserObject))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            if (id != "500" && id != "404" && id != "400")
            {

                return Request.CreateResponse(HttpStatusCode.OK, new List<PrescriptionObject>());
            }
            else if (id == "400")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "资料格式有误")
                    ;

            }
            else if (id == "404")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "查无此用户")
                    ;

            }
            else

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }


        /// <summary>
        /// 修改该使用者资讯(包含删除
        /// </summary>
        /// <param name="id">使用者的 id</param>
        /// <param name="body">所要查询的四诊记录的主症</param>
        /// <remarks>
        /// 更新成功
        /// </remarks>
        /// <response code="200">更新成功</response>
        /// <response code="400">资料格式有误</response>
        /// <response code="404">查无此用户</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Put(string id, [FromBody]UserObject value)
        {
            if (id != "500" && id != "404" && id != "400")
            {

                return Request.CreateResponse(HttpStatusCode.OK, new List<PrescriptionObject>());
            }
            else if (id == "400")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "资料格式有误")
                    ;

            }
            else if (id == "404")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "查无此用户")
                    ;

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
