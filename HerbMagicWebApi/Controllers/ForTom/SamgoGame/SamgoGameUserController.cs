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
using static HerbMagicWebApi.Models.ObjectModels;
using static HerbMagicWebApi.Models.SamgoModels;
using static HerbMagicWebApi.Models.ViewModels;

namespace HerbMagicWebApi.Controllers.ForTom
{
    public class SamgoGameUserController : ApiController
    {

        public string TableName = "SamgoGame";

        [HttpPost]
        [Route("api/v1/SamgoGameUser")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Post([FromBody]All userData)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                SamgoGameHelper.AddUserData(userData));

            //UserBuild ub = new UserBuild();
            //userData.Time = Function.GetUTCTime();
            //SamgoModels.UserInfo ui = new SamgoModels.UserInfo();
            //UserValue uLord = new UserValue();
            //UserValue uProsperity = new UserValue();
            //UserPower up = new UserPower();
            //UserTechnique ut = new UserTechnique();
            //UserOrdnance uo = new UserOrdnance();

            //var userInfo = new SamgoModels.UserInfo();
            //userData.SeqNo = DapperHelper.Search<int>(
            //   connectionString, " exec [samgo_add_user_and_group] N'" +
            //   userData.Role + "' , N'" + userData.Legion + "'").FirstOrDefault();
            //ConvertDataToModels(ref ub, ref ui, ref uLord, ref uProsperity, ref up, userData);

            //DapperHelper.InsertSQLNormal<UserBuild>(connectionString, "Build", ub);
            ////DapperHelper.InsertSQL<SamgoModels.UserInfo>(connectionString, "SamgoGame", ui);
            //DapperHelper.InsertSQLNormal<UserValue>(connectionString, "Lord", uLord);
            //DapperHelper.InsertSQLNormal<UserValue>(connectionString, "Prosperity", uProsperity);
            //DapperHelper.InsertSQLNormal<UserPower>(connectionString, "SamgoGamePower", up);
            //ConvertDataToModels(ref ut, ref uo, userData);

            //DapperHelper.InsertSQLNormal<UserTechnique>(connectionString, "Technique", ut);
            //var response = DapperHelper.InsertSQLNormal<UserOrdnance>(connectionString, "Ordnance", uo);

            //return Request.CreateResponse(HttpStatusCode.OK, userData.SeqNo);
        }

      
        //[HttpPost]
        //[Route("api/v1/SamgoGameUserDetail")]
        //[SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        //public HttpResponseMessage PostDetail([FromBody]UserDetail userData)
        //{
        //    userData.Time = Function.GetUTCTime();
        //    UserTechnique ut = new UserTechnique();
        //    UserOrdnance uo = new UserOrdnance();

        //    ConvertDataToModels(ref ut, ref uo, userData);

        //     DapperHelper.InsertSQLNormal<UserTechnique>(connectionString, "Technique", ut);
        //    var response = DapperHelper.InsertSQLNormal<UserOrdnance>(connectionString, "Ordnance", uo);
        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}

        [HttpPost]
        [Route("api/v1/SamgoGameUserSearch")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage PostUserSearch(Inner data)
        {
            AllString ob = new AllString();
            try
            {
                UserDetail uDetail = new UserDetail();
                UserData ud = DapperHelper.Search<UserData>(SamgoGameHelper.connectionString,
                    "exec [samgo_get_user_info] N'" + data.Role + "', N'" + data.Legion + "'").FirstOrDefault();
                uDetail = DapperHelper.Search<UserDetail>(SamgoGameHelper.connectionString,
                    "exec [samgo_get_user_tech_info] N'" + data.Role + "', N'" + data.Legion + "'").FirstOrDefault();
                SamgoGameHelper.ConvertDataToModels(ref ob, ud, uDetail);
            }
            catch (Exception) { }
            if (ob.Role == null)
            {
                ob.Role = data.Role;
                ob.Legion = data.Legion;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ob);
        }

        [HttpGet]
        [Route("api/v1/SamgoGameUserDetail")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]

        public HttpResponseMessage Get()
        {

            var allData = DapperHelper.Search<AllString>(SamgoGameHelper.connectionString,
               "exec [samgo_get_user_info_list]");
            int count = 3;
            return Request.CreateResponse(HttpStatusCode.OK, SamgoGameHelper.FillDataForView(allData, count));

        }

        

      
        public class Inner
        {
            public string Role;
            public string Legion;
        }
    }
}
