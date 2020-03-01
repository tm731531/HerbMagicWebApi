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
    public class membershipController : ApiController
    {
       
        [Route("api/v1/password/activate")]
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="userId">user_phone - 使用者手机号码</param>
        /// <remarks>
        /// 验证码传送成功
        /// </remarks>
        /// <response code="200">验证码传送成功</response>
        /// <response code="404">查无此独家秘方</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(DataObject))]
        [SwaggerOperation()]
        public HttpResponseMessage Get(string  userId)
        {
            if (userId != "500" && userId != "400")
            {
                DataObject Do = new DataObject();
                Do.data = "10312";
                return Request.CreateResponse(HttpStatusCode.OK, Do);
            }
            else if (userId == "400")

            {
                DataObject Do = new DataObject();
                Do.data = "10310";

                // log exception here
                return Request.CreateResponse(HttpStatusCode.NotFound, Do)
                    ;

            }
            else

            {
                DataObject Do = new DataObject();
                Do.data = "10311";

                // log exception here
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Do)
                    ;

            }
        }

        [Route("api/v1/password/activated")]
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// 验证成功
        /// </remarks>
        /// <response code="200">验证成功</response>
        /// <response code="400">(200) request 格式错误 / 验证码已经过期，请重新验证</response>
        /// <response code="500">(500) server error 验证失败</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(DataObject))]
        [SwaggerOperation()]
        public HttpResponseMessage Post([FromBody]MemberObject body)
        {
            if(body.user_code != "500" &&  body.user_code != "400")
            {
                DataObject err = new DataObject();
                err.data = "10316";

                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            else if (body.user_code == "400")
            {
                DataObject err = new DataObject();
                err.data = "10313 / 10314";

                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            
            }
           
            else
            {

                DataObject err = new DataObject();
                err.data = "10315";

                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);

               
            }
        }



        [Route("api/v1/signup")]
        /// <summary>
        /// 会员注册(example body 为 医师
        /// </summary>
        /// <param name="role"></param>
        /// <param name="body"></param>
        /// <remarks>
        /// 验证成功
        /// </remarks>
        /// <response code="200">验证成功</response>
        /// <response code="302">(200)已有此用户</response>
        /// <response code="400">(200)注册 request格式不对</response>
        /// <response code="500">(500) server error 验证失败</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.Found, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [SwaggerOperation()]
        public HttpResponseMessage SignUp([FromUri] string role, [FromBody]DocObject body)
        {
            if (role != "500" && role != "400" && role != "302")
            {
                DataObject err = new DataObject();
                err.data = "10101";
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            else if (role == "302")
            {
                DataObject err = new DataObject();
                err.data = "10103";
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else if (role == "400")
            {
                DataObject err = new DataObject();
                err.data = "10104";
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else
            {
                Error err = new Error();
                err.message = "系统维护中";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
        }



        [Route("api/v1/signin")]
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="role"></param>
        /// <param name="body"></param>
        /// <remarks>
        /// 登入成功
        /// </remarks>
        /// <response code="200">登入成功</response>
        /// <response code="400">(200)登入失败：格式错误 / 使用者或密码错误 / user_cert 栏位验证失败</response>
        /// <response code="500">(500) server error 验证失败</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.Found, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [SwaggerOperation()]
        public HttpResponseMessage SignIn([FromUri] string role, [FromBody]DocObject body)
        {
            if (role != "500" && role != "400" )
            {
                DataObject err = new DataObject();
                err.data = "10206";
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            else if (role == "400")
            {
                DataObject err = new DataObject();
                err.data = "10207 / 10208 / 10209";
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else
            {
                Error err = new Error();
                err.message = "系统维护中";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
        }

        [Route("api/v1/password/change")]
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// 登入成功
        /// </remarks>
        /// <response code="200">登入成功</response>
        /// <response code="400">(200)request格式错误 / 用户手机手机号错误 / 用户password错误</response>
        /// <response code="500">(500) server error 密码更改失败</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.Found, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(DataObject))]
        [SwaggerOperation()]
        public HttpResponseMessage PasswordChange( [FromBody]PasswordChangeObject body)
        {
            if (body.new_password != "500" && body.new_password != "400")
            {
                DataObject err = new DataObject();
                err.data = "10403";
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            else if (body.new_password == "400")
            {
                DataObject err = new DataObject();
                err.data = "10410 / 10411 / 10412 ";
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else
            {
                DataObject err = new DataObject();
                err.data = "10404";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
        }



        [Route("api/v1/password/forget")]
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// 验证码验证合格，新密码更新成功
        /// </remarks>
        /// <response code="200">验证码验证合格，新密码更新成功</response>
        /// <response code="400">(200)request格式错误/ 用户手机手机号错误/验证码已经过期，请重新验证/验证失败</response>
        /// <response code="500">(500) server error 密码更改失败</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.Found, Type = typeof(DataObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(DataObject))]
        [SwaggerOperation()]
        public HttpResponseMessage PasswordForget([FromBody]PasswordForgetObject body)
        {
            if (body.new_password != "500" && body.new_password != "400")
            {
                DataObject err = new DataObject();
                err.data = "10403";
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            else if (body.new_password == "400")
            {
                DataObject err = new DataObject();
                err.data = "10510 / 10511 / 10512 / 10513";
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else
            {
                DataObject err = new DataObject();
                err.data = "10404";
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
        }
    }
}
