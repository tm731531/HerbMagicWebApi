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

    /// <summary>
    /// 就诊纪录
    /// </summary>

    public class prescriptionController : ApiController
    {
        /// <summary>
        /// 查询单一就诊纪录
        /// </summary>
        /// <param name="id">_id - 就诊纪录 id</param>
        /// <remarks>
        /// 成功获得就诊纪录资讯
        /// </remarks>
        /// <response code="200">成功获得就诊纪录资讯</response>
        /// <response code="404">查无此就诊纪录</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PrescriptionObject))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [SwaggerOperation()]
        // GET: api/formula/5
        public HttpResponseMessage Get(string id)
        {
            if (id != "500"&&id != "404")
            {
               
                return Request.CreateResponse(HttpStatusCode.OK, new  PrescriptionObject());
            }
            else if (id ==  "404")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "查无此就诊纪录")
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
        /// 新增单一就诊纪录
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// OK
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PrescriptionObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        // POST: api/formula
        public HttpResponseMessage Post([FromBody]PrescriptionObject body)
        {
            if (body.doctor_id != "500")
            {
                 return Request.CreateResponse(HttpStatusCode.OK, body);
            }
            
            else

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }

        /// <summary>
        /// 修改 (包含删除)
        /// </summary>
        /// <param name="id">_id - 该独家秘方 id</param>
        /// <param name="body"></param>
        /// <remarks>
        /// 更新成功
        /// </remarks>
        /// <response code="200">更新成功</response>
        /// <response code="404">资料格式错误</response>
        /// <response code="400">查无此资料</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        // PUT: api/formula/5
        public HttpResponseMessage Put(string id, [FromBody]PrescriptionObject body)
        {
            if (id != "500" && id != "404" && id != "400")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Error() {  message= "更新成功" });
            }
            else if (id == "404")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "查无此资料")
                    ;

            }
            else if (id == "400")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "资料格式错误")
                    ;

            }
            else

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }


        [Route("api/v1/prescriptions")]
        /// <summary>
        /// 查询多个就诊纪录
        /// </summary>
        /// <param name="user_id">doc_id - 医师 id</param>
        /// <param name="q">所要查询的四诊记录的主症</param>
        /// <param name="count">要查询的页码</param>
        /// <param name="order">正序带 descending & 倒序带 ascending</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>
        /// <response code="404">查无此用户</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PrescriptionObject[]))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [HttpGet]

        public HttpResponseMessage Gets([FromUri]string user_id, [FromUri] string q, [FromUri] int count ,[FromUri] string order)
        {
            if (user_id != "500" && user_id != "404")
            {
               
                return Request.CreateResponse(HttpStatusCode.OK, new List<PrescriptionObject>());
            }
            else if (user_id == "404")

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

        [Route("api/v1/prescriptions/count")]
        /// <summary>
        /// 查询所符合条件的就诊纪录的数量
        /// </summary>
        /// <param name="user_id">doc_id - 医师 id</param>
        /// <param name="q">所要查询的四诊记录的主症</param>
        /// <param name="count">要查询的页码</param>
        /// <param name="order">正序带 descending & 倒序带 ascending</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Error))]
        [HttpGet]

        public HttpResponseMessage GetsCount([FromUri]string ob, [FromUri] string formula_treat, [FromUri] int count, [FromUri] string order)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Error() { message = "xx" });
        }

        
    }
}
