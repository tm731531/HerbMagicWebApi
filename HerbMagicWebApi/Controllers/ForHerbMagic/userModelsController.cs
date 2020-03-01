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
    public class userModelsController : ApiController
    {
       
        // POST: api/userModels
        [Route("v1/user/models")]
        /// <summary>
        /// 此API用于选择用户模型
        /// </summary>
        /// <param name="authToken">session  UUID</param>
        /// <param name="modelFlags">该session启用模型</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]UserModelsObject umo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new Object());
        }
        [Route("v1/user/qa")]
        /// <summary>
        /// 此API用于进行QA
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <param name="ignored">ignored</param>
        /// <param name="posAnswer">正面指向的证</param>
        /// <param name="negAnswer">负面指向的证</param>
        /// <param name="wideMode">是否使用wide模式</param>
        /// <param name="horizontalLimit">qa返回结果横向数量</param>
        /// <param name="verticalLimit">qa返回结果纵向数量</param>
        /// <param name="inputLength">wideMode时内置graph使用比例</param>
        /// <param name="bindingStartPoint">deepMode时返回结果参考起始点</param>
        /// <param name="bindingLength">deepMode时返回结果参考长度</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserQa([FromBody]UserQaObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new UserQaResponseObject());
        }

        [Route("v1/user/specials")]
        /// <summary>
        /// 此API用于获得当前初始证列表
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserSpecials([FromBody]UserSpecialsObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
        }

        [Route("v1/user/status")]
        /// <summary>
        /// 此API用于获得用户当前session状态
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserStatus([FromBody]UserSpecialsObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new UserStatusResponseObject());
        }
        [Route("v1/user/status/update")]
        /// <summary>
        /// 此API用于获得用户当前session状态
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <param name="posAnswer">要删除的正面指向证</param>
        /// <param name="negAnswer">要删除的负面指向证</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserStatusUpdate([FromBody]UserStatusUpdateObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new object());
        }

        [Route("v1/user/diagnosis")]
        /// <summary>
        /// 此API用于对用户当前session进行诊断
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <param name="limit">该次诊断返回长度</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserDiagnosis([FromBody]UserDiagnosisObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new UserDiagnosisResponseObject());
        }

        [Route("v1/user/query")]
        /// <summary>
        /// 此API用于获得且filter该session上一次诊断结果
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <param name="limit">该次诊断返回长度</param>
        /// <param name="authors">返回哪几个作者的结果</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserQuery([FromBody]UserQueryObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new UserDiagnosisResponseObject());
        }


        [Route("v1/user/status/delete")]
        /// <summary>
        /// 此API用于获得用户当前session状态
        /// </summary>
        /// <param name="authToken">session UID</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpPost]
        public HttpResponseMessage UserStatusDelete([FromBody]UserSpecialsObject uqo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, new object());
        }

        [Route("v1/dev/version")]
        /// <summary>
        /// 此API用于获得用户当前session状态
        /// </summary>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>       
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = null)]
        [HttpGet]
        public HttpResponseMessage DevVersion()
        {

            return Request.CreateResponse(HttpStatusCode.OK, new GetVersionObject());
        }
    }
}
