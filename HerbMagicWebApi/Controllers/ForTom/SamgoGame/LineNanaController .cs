using HerbMagicWebApi.Common;
using HerbMagicWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static HerbMagicWebApi.Models.ViewModels;

namespace HerbMagicWebApi.Controllers.ForTom
{
    public class LineNanaController : ApiController
    {

        private string channelSecret = "7ca1c4670c2d5d8160d7bd6c85321c00";

        // GET: api/Line/5
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST: api/Line
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                var signature = Request.Headers.GetValues("X-Line-Signature").FirstOrDefault();
                var body = await Request.Content.ReadAsStringAsync();
                var cryptoResult = SHA256Crypto(body);
                if (signature == cryptoResult)
                {
                    var value = JsonConvert.DeserializeObject<WebhookModel>(body);
                    var handler = NanaFactory.CreateLineHandler();
                    await handler.ProcessMessage(value);
                }
                else
                {
                    // signature not valid
                }
            }
            catch (Exception ex)
            {
                // handle exception
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        private string SHA256Crypto(string text)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(channelSecret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Convert.ToBase64String(hash);
            }
        }
    }

    public class NanaFactory
    {
        public static ILineHandler CreateLineHandler() => new NanaLineHandler();
    }
    
    public class NanaLineHandler : ILineHandler
    {
        public static string connectionString =
             ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        public async Task ProcessMessage(WebhookModel value)
        {
            foreach (var msg in value.events)
            {
                switch (msg.type)
                {
                    case "follow":
                        await NanaAPIHelper.ReplyMessage(GetTextReply(msg.replyToken, "follow event type"));
                        break;
                    case "postback":
                        await NanaAPIHelper.ReplyMessage(GetTextReply(msg.replyToken, msg.postback.data));
                        break;
                    case "join":
                        await NanaAPIHelper.ReplyMessage(GetTextReply(msg.replyToken, "join event type"));
                        break;
                    case "message":
                        var res = await NanaAPIHelper.ReplyMessage(HandleMessageObject(msg));
                        break;
                    default:
                        await NanaAPIHelper.ReplyMessage(GetTextReply(msg.replyToken, "not support"));
                        break;
                }
            }
        }

        private ReplyModel HandleMessageObject(Event msg)
        {
            switch (msg.message.type)
            {
                case "sticker":
                    Random rnd = new Random();
                    return GetStickerReply(msg.replyToken, "1", rnd.Next(1, 18).ToString());
                case "text":
                    return GetTextReply(msg);
                case "image":
                    return GetTextReply(msg.replyToken, "img");
                case "video":
                    return GetTextReply(msg.replyToken, "video");
                case "audio":
                    return GetTextReply(msg.replyToken, "audio");
                case "location":
                    return GetTextReply(msg.replyToken, $"area {msg.message.latitude},{msg.message.longitude}", msg);
                default:
                    return GetTextReply(msg.replyToken, msg.message.type);
            }
        }
        private ReplyModel GetTextReply(Event msg)
        {
            return LineHelper.NanaSamgoReply(msg);

        }
        private ReplyModel GetTextReply(string token, string text, Event msg = null)
        {
            
            return LineHelper.NaNaSamgoReply(token, text, msg);
            
            //if (text.StartsWith("戰力"))
            //{
            //    var q = text.Split(' ')[1].Trim();
            //    var response = DapperHelper.Search<UserInfo>(
            //        connectionString,
            //        $" SELECT SamgoGame.[Role], SamgoGame.[Official], SamgoGame.[Legion], SamgoGamePower.[Power] , SamgoGamePower.[Time] FROM SamgoGame INNER JOIN SamgoGamePower ON SamgoGamePower.SeqNo = SamgoGame.SeqNo where SamgoGame.[Role] like N'%{q}%';"
            //        ).OrderByDescending(x => x.Power);
            //    var temp = "";
            //    temp += response.FirstOrDefault().Role + "\n";
            //    foreach (var a in response)
            //    {
            //        temp += a.Power;
            //        temp += " " + a.Time;
            //        temp += "\n";
            //    }
            //    return new ReplyModel()
            //    {
            //        replyToken = token,
            //        messages = new List<ReplyMessage>()
            //    {
            //        new ReplyMessage()
            //        {
            //            type = "text",
            //            text=temp
            //        }
            //    }
            //    };
            //}
            //else
            //{
            //    return new ReplyModel()
            //    {
            //        replyToken = token

            //    };
            //}
        }

        private ReplyModel GetStickerReply(string token, string package, string sticker)
        {
            return new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    //new ReplyMessage()
                    //{
                    //    type = "text",
                    //    text = "token="+token
                    //},
                    //new ReplyMessage()
                    //{
                    //    type = "sticker",
                    //    packageId = package,
                    //    stickerId = sticker
                    //}
                }
            };
        }
    }
    public class NanaAPIHelper
    {
        private readonly static string channelToken = "VQWLnP4niUkAVYm0n+DemaW0i3/5zFOwWTNSVDVh02N5WOsrfauPCbYNQtjjy8/m1TVhuc0KtJ4o7cyxOEAB8jbvQ2s8pIltAzNRwcmora2jwGx8KWMwpbIhjw63COQSZWnGvU0QpT0D2ZOfQK7ypAdB04t89/1O/w1cDnyilFU=";

        public static async Task<HttpResponseMessage> ReplyMessage(ReplyModel reply)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(reply);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {channelToken}");
                return await client.PostAsync("https://api.line.me/v2/bot/message/reply",
                    new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}
