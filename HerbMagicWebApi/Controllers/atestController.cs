
using Newtonsoft.Json;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HerbMagicWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
    
    public class atestController : ApiController
    {
        // GET: api/atest
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/atest/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/atest
        /// <summary>
        /// 只是測試而已
        /// </summary>
        /// <param name="value">測試資料</param>
        /// <remarks>Sample request:
        ///     POST /Todo
        /// 
        ///     {   
        ///         Id = 123456,
        ///         Name = "yao",
        ///         Age = 19
        ///     }
        /// </remarks>
        /// <example>GG</example>
        /// <seealso cref="Put"/>
        /// <returns>回傳</returns>
        /// <response code="204">成功轉向</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(vasle), typeof(MemberExamplesProvider))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(MemberExamplesProvider))]
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(GroupName ="V2")]
        public string Post([FromBody] vasle value)
        {
            return value.Name+ value.Age.ToString()+ value.Id.ToString();
        }

        // PUT: api/atest/5
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(GroupName = "V3")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/atest/5
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(GroupName = "V1")]
        public void Delete(int id)
        {
        }

        public class vasle
        {
            /// <summary>
            /// id
            /// </summary>
            /// 
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Age { get; internal set; }
        }
        public class MemberExamplesProvider : IExamplesProvider
        {
            public object GetExamples()
            {
                return new vasle
                {
                    Id = Guid.NewGuid(),
                    Name = "yao",
                    Age = 19
                };
            }
        }
    }
}
