using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HerbMagicWebApi.Controllers.ForArthur
{
    public class CoreController : ApiController
    {

        /*
         1.驗證碼發送機制(SMS) &gt;&gt;中華電信 Twilo https://www.twilio.com/console/sms/settings/geo-permissions
2.驗證碼發送機制(Email) &gt;&gt;SMTP or IFTTT  https://www.ragic.com/intl/zh-TW/home
3.發送驗證信 &gt;&gt;IFTTT
5.加密保護 (前端 後端各一)&gt;&gt;RSA DES
6.資料進入資料庫前保護 &gt;&gt;混淆TOKEN
7.圖片驗證碼 &gt;&gt;數字產圖
10.串接外部登入系統(OLAP) &gt;&gt; FB GOOGLE 登入 註冊
11.控制所有API的權限控管 &gt;&gt;控制API的API
我以開發這些來TRYTRY           
             */
        // GET: api/Core
        public IEnumerable<string> Get()
        {

           yield  return "";
           
        }

        // GET: api/Core/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Core
        /// <summary>
        /// 使用這家SMS通訊https://www.twilio.com/console/sms/settings/geo-permissions
        /// </summary>
        /// <param name="value">輸入要送的對象跟內容</param>
        public void Post([FromBody]SMSObject value)
        {

            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC1dcc5b8f80aa07b5d439577488d30152";
            const string authToken = "17f92a9e91207b997b1293edeb1c85a7";

            TwilioClient.Init(accountSid, authToken);
            
            var message = MessageResource.Create(
                body: $"小丁測試的 This is test {value.msg} 091548 !QAZ#@$%^&**((",
                from: new Twilio.Types.PhoneNumber("+18572930994"),
                to: new Twilio.Types.PhoneNumber($"{ value.phone }")
            );
        }

        // PUT: api/Core/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Core/5
        public void Delete(int id)
        {
        }
    }
}
