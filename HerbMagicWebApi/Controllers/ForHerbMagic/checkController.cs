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
    public class checkController : ApiController
    {


        /// <summary>
        /// 验证知识库request
        /// </summary>
        /// <param name="b"  Type="header"></param>
        /// <remarks>
        /// 成功获得医师资讯
        /// </remarks>
        /// <response code="200">成功获得医师资讯</response>
        /// <response code="403">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(Error))]
        public HttpResponseMessage Get()
        {

            try {
                Error re = new Error();
                re.message = "check success";
                 return Request.CreateResponse(HttpStatusCode.OK, re);
            }
            catch
            {
                Error re = new Error();
                re.message = "check fail";
                return Request.CreateResponse(HttpStatusCode.Forbidden, re);
            }
        }

       
    }
}
