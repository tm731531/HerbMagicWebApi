using HerbMagicWebApi.Common;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using static HerbMagicWebApi.Models.ObjectModels;

namespace HerbMagicWebApi.Controllers
{
    /// <summary>
    /// 醫師
    /// </summary>
    public class doctorController : ApiController
    {

        /// <summary>
        /// 获得该医师资讯
        /// </summary>
        /// <param name="id">user_phone - 该医师手机号码</param>
        /// <remarks>
        /// 成功获得医师资讯
        /// </remarks>
        /// <response code="200">成功获得医师资讯</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DocObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(string id)
        {
            string connectionString =
                   ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT *  FROM [dbo].[Member]  where _id = " + id;

            if (id != "500")
            {
                DocObject ro = new DocObject();
               
                DataSet tempTable = DBHelper.Search(connectionString, queryString);
                foreach (DataRow pRow in tempTable.Tables["tempTable"].Rows)
                {
                    ro.doc_add_1 = DBHelper.GetValue<string>(pRow, "doc_add_1"); ; 
                    ro.doc_nickname = DBHelper.GetValue<string>(pRow, "doc_nickname"); 
                    ro.doc_verification = DBHelper.GetValue<bool>(pRow, "doc_verification");
                    ro.doc_add_2 = DBHelper.GetValue<string>(pRow, "doc_add_2");
                    ro.doc_password_time = DBHelper.GetValue<int>(pRow, "doc_password_time");
                    ro.doc_sign = DBHelper.GetValue<string>(pRow, "doc_sign");
                    ro.doc_gender = DBHelper.GetValue<string>(pRow, "doc_gender");
                    ro.doc_forbid_time = DBHelper.GetValue<int>(pRow, "doc_forbid_time");
                    ro.doc_email = DBHelper.GetValue<string>(pRow, "doc_email");
                    ro.doc_active_time = DBHelper.GetValue<int>(pRow, "doc_active_time");
                    ro.doc_agreement = DBHelper.GetValue<string>(pRow, "doc_agreement");
                    ro.doc_name = DBHelper.GetValue<string>(pRow, "doc_name");
                    ro.user_phone = DBHelper.GetValue<string>(pRow, "user_phone");
                    ro.doc_level = DBHelper.GetValue<string>(pRow, "doc_level");
                    ro.doc_hospital = DBHelper.GetValue<string>(pRow, "doc_hospital");
                    ro.doc_birth = DBHelper.GetValue<string>(pRow, "doc_birth");
                    ro.doc_account_status = DBHelper.GetValue<string>(pRow, "doc_account_status");
                    ro.doc_license_tcm_url = DBHelper.GetValue<string>(pRow, "doc_license_tcm_url");
                    ro.doc_identity = DBHelper.GetValue<string>(pRow, "doc_identity");
                    ro.doc_cert = DBHelper.GetValue<bool>(pRow, "doc_cert");
                    ro.doc_department = new string[2] { "骨科", "骨科" };
                    ro.doc_id = DBHelper.GetValue<string>(pRow, "doc_id");
                    ro.doc_license_url = DBHelper.GetValue<string>(pRow, "doc_license_url");
                    ro.doc_photo = DBHelper.GetValue<string>(pRow, "doc_photo");
                    ro.doc_location = DBHelper.GetValue<string>(pRow, "doc_location");
                    ro.doc_level_option = DBHelper.GetValue<string>(pRow, "doc_level_option");
                    ro.doc_date = DBHelper.GetValue<int>(pRow, "doc_date");
                    ro.doc_status = DBHelper.GetValue<string>(pRow, "doc_status");
                    ro._id = DBHelper.GetValue<string>(pRow, "_id");
                    ro.doc_identity_option = DBHelper.GetValue<string>(pRow, "doc_identity_option");
                    ro.doc_syndromes = new Doc_Syndromes[1] { new Doc_Syndromes() { key = "头痛", value = new string[1] { "咽喉" } } };
                    ro.doc_unionid = DBHelper.GetValue<string>(pRow, "doc_unionid");
                    ro.doc_models = new Doc_Models[1] { new Doc_Models() { date = new int[1] { 153198923 }, model = "zzj" } };

                }

                return Request.CreateResponse(HttpStatusCode.OK, ro);
            }
            else
            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统维护中")
                    ;

            }
        }



        /// <summary>
        /// 修改该医师资讯(包含删除)
        /// </summary>
        /// <param name="id">_id - 该医师的 id</param>
        /// <param name="body"></param>
        /// <remarks>
        /// 更新成功
        /// </remarks>
        /// <response code="200">更新成功</response>
        /// <response code="400">资料格式有误</response>
        /// <response code="404">查无此用户</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [HttpPut]
        public HttpResponseMessage Put(string id, [FromBody]DocObject body)
        {
            if (id != "500" && id != "404" && id != "400")
            {
                Error err = new Error();
                err.message = "更新成功";

                return Request.CreateResponse(HttpStatusCode.OK, err);
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
