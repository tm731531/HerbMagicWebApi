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
    /// 秘方
    /// </summary>
    
    public class formulaController : ApiController
    {
        /// <summary>
        /// 查询单一独家秘方
        /// </summary>
        /// <param name="id">_id - 该独家秘方 id</param>
        /// <remarks>
        /// 成功获得独家秘方资讯
        /// </remarks>
        /// <response code="200">成功获得独家秘方资讯</response>
        /// <response code="404">查无此独家秘方</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FormulaObject))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [SwaggerOperation()]
        // GET: api/formula/5
        public HttpResponseMessage Get(string id)
        {
            if (id != "500"&&id != "404")
            {
                FormulaObject fo = new FormulaObject();
                fo.formula_name = "真武汤";
                fo.create_time = 153198923;
                fo.formula_delete = false;
                fo.taboo = "少阴阳气衰，已见下利清谷，脉象微欲绝等证，误发其汗，必致厥逆亡阳，因此应用本方时要严加注意。";
                fo.formula_ingredient = new string[2][] { new string[1] { ""},new string[1] {""}};
                fo.usage = "配水喝";
                fo.doc_id = "556677";
                fo.formula_treat = new string[2] { "阳虚水泛证", "阳虚水泛证" };
                fo.follow_up = "要配温开水";
                fo.pathogenesis = "吹凉风";
                fo.last_edit_time = 153298923;
                fo.formula_use = true;
                fo.effect = "头好壮壮";
                fo.formula_delete_time = 153298923;
                fo.formula_type = new string[2] { "order", "order" };
                fo._id = "223344";
                fo.side_effect = "误发其汗";

                return Request.CreateResponse(HttpStatusCode.OK, fo);
            }
            else if (id ==  "404")

            {

                // log exception here
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "查无此独家秘方")
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
        /// 新增单一独家秘方
        /// </summary>
        /// <param name="body"></param>
        /// <remarks>
        /// 创建成功
        /// </remarks>
        /// <response code="200">创建成功</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FormulaObject))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        // POST: api/formula
        public HttpResponseMessage Post([FromBody]FormulaObject body)
        {
            if (body.create_time != 500 && body.create_time != 404)
            {
                FormulaObject fo = new FormulaObject();
                fo.formula_name = "真武汤";
                fo.create_time = 153198923;
                fo.formula_delete = false;
                fo.taboo = "少阴阳气衰，已见下利清谷，脉象微欲绝等证，误发其汗，必致厥逆亡阳，因此应用本方时要严加注意。";
                fo.formula_ingredient = new string[2][] { new string[1] { "" }, new string[1] { "" } };
                fo.usage = "配水喝";
                fo.doc_id = "556677";
                fo.formula_treat = new string[2] { "阳虚水泛证", "阳虚水泛证" };
                fo.follow_up = "要配温开水";
                fo.pathogenesis = "吹凉风";
                fo.last_edit_time = 153298923;
                fo.formula_use = true;
                fo.effect = "头好壮壮";
                fo.formula_delete_time = 153298923;
                fo.formula_type = new string[2] { "order", "order" };
                fo._id = "223344";
                fo.side_effect = "误发其汗";

                return Request.CreateResponse(HttpStatusCode.OK, fo);
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
        /// 更新到的新的独家秘方资讯
        /// </remarks>
        /// <response code="200">更新到的新的独家秘方资讯</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FormulaObject))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        // PUT: api/formula/5
        public HttpResponseMessage Put(string id, [FromBody]string value)
        {
            if (id != "500" && id != "404" && id != "400")
            {
                FormulaObject fo = new FormulaObject();
                fo.formula_name = "真武汤";
                fo.create_time = 153198923;
                fo.formula_delete = false;
                fo.taboo = "少阴阳气衰，已见下利清谷，脉象微欲绝等证，误发其汗，必致厥逆亡阳，因此应用本方时要严加注意。";
                fo.formula_ingredient = new string[2][] { new string[1] { "" }, new string[1] { "" } };
                fo.usage = "配水喝";
                fo.doc_id = "556677";
                fo.formula_treat = new string[2] { "阳虚水泛证", "阳虚水泛证" };
                fo.follow_up = "要配温开水";
                fo.pathogenesis = "吹凉风";
                fo.last_edit_time = 153298923;
                fo.formula_use = true;
                fo.effect = "头好壮壮";
                fo.formula_delete_time = 153298923;
                fo.formula_type = new string[2] { "order", "order" };
                fo._id = "223344";
                fo.side_effect = "误发其汗";

                return Request.CreateResponse(HttpStatusCode.OK, fo);
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


        [Route("api/v1/formulas")]
        /// <summary>
        /// 查询多个独家秘方
        /// </summary>
        /// <param name="id">doc_id - 医师 id</param>
        /// <param name="formula_treat">独家秘方主治</param>
        /// <param name="count">分页所要查询的具体页数</param>
        /// <param name="order">正序带 descending & 倒序带 ascending</param>
        /// <remarks>
        /// 查询成功
        /// </remarks>
        /// <response code="200">查询成功</response>
        /// <response code="404">查无此用户</response>
        /// <response code="500">系统维护中</response>
        /// <returns >HttpResponseMessage</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FormulaObject[]))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(Error))]
        [HttpGet]

        public HttpResponseMessage Gets([FromUri]string id, [FromUri] string formula_treat, [FromUri] int count ,[FromUri] string order)
        {
            if (id != "500" && id != "404")
            {
                List<FormulaObject> lfo = new List<FormulaObject>();
                FormulaObject fo = new FormulaObject();
                fo.formula_name = "真武汤";
                fo.create_time = 153198923;
                fo.formula_delete = false;
                fo.taboo = "少阴阳气衰，已见下利清谷，脉象微欲绝等证，误发其汗，必致厥逆亡阳，因此应用本方时要严加注意。";
                fo.formula_ingredient = new string[2][] { new string[1] { "" }, new string[1] { "" } };
                fo.usage = "配水喝";
                fo.doc_id = "556677";
                fo.formula_treat = new string[2] { "阳虚水泛证", "阳虚水泛证" };
                fo.follow_up = "要配温开水";
                fo.pathogenesis = "吹凉风";
                fo.last_edit_time = 153298923;
                fo.formula_use = true;
                fo.effect = "头好壮壮";
                fo.formula_delete_time = 153298923;
                fo.formula_type = new string[2] { "order", "order" };
                fo._id = "223344";
                fo.side_effect = "误发其汗";
                lfo.Add(fo);

                return Request.CreateResponse(HttpStatusCode.OK, lfo);
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

        [Route("api/v1/formulas/count")]
        /// <summary>
        /// 查询符合query的资料的数量
        /// </summary>
        /// <param name="id">doc_id - 医师 id</param>
        /// <param name="formula_treat">独家秘方主治</param>
        /// <param name="count">分页所要查询的具体页数</param>
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
