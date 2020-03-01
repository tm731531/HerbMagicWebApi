using HerbMagicWebApi.Common;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HerbMagicWebApi.Models.ObjectModels;
using static HerbMagicWebApi.Models.ViewModels;

namespace HerbMagicWebApi.Controllers.ForTom
{
    public class SamgoGameController : ApiController
    {
        public static string connectionString =
            ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
        public string TableName = "SamgoGame";

        [HttpPost]
        [Route("api/v1/SamgoGame")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        public HttpResponseMessage Get(Times time)
        {
            var response = DapperHelper.Search<SPget_samgo_power_list_diff>(
                connectionString,
              //  " SELECT SamgoGame.[Role], SamgoGame.[Official], SamgoGame.[Legion], SamgoGamePower.[Power] , SamgoGamePower.[Time] FROM SamgoGame INNER JOIN SamgoGamePower ON SamgoGamePower.SeqNo = SamgoGame.SeqNo;"
              " exec [get_samgo_power_list_diff] '"+ time.firstTime + "','"+ time.secondTime + "'"
                
                );
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        public class Times
        {
            public string secondTime;
            public string firstTime;
        }

      

    }
}
