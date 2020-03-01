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
    public class LineController : ApiController
    {

        private string channelSecret = "d728dbf2aa859bc73f942d9042e5c37c";

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
                    var handler = Factory.CreateLineHandler();
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

    public class Factory
    {
        public static ILineHandler CreateLineHandler() => new LineHandler();
    }

    public interface ILineHandler
    {
        Task ProcessMessage(WebhookModel value);
    }
    public class LineHandler : ILineHandler
    {
        public static string connectionString =
             ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        public async Task ProcessMessage(WebhookModel value)
        {
          //  await APIHelper.ReplyMessage(GetJsonReply(value));
            foreach (var msg in value.events)
            {
                switch (msg.type)
                {
                    //case "follow":
                    //    await APIHelper.ReplyMessage(GetFollowReply(msg));
                    //    break;
                    case "postback":
                        await APIHelper.ReplyMessage(GetPostbackReply(msg));
                        break;
                    //case "memberJoined":
                    //    await APIHelper.ReplyMessage(GetmemberJoinReply(msg));
                    //    break;
                    //case "memberLefted":
                    //    await APIHelper.ReplyMessage(GetMemberLeftReply(msg));
                    //    break;
                    //case "unfollow":
                    //    await APIHelper.ReplyMessage(GetUnfollowReply(msg));
                    //    break;
                    //case "leave":
                    //    await APIHelper.ReplyMessage(GetLeaveReply(msg));
                    //    break;

                    case "message":
                        var res = await APIHelper.ReplyMessage(HandleMessageObject(msg));
                        break;
                    default:
                        await APIHelper.ReplyMessage(GetTextReply(msg.replyToken, "not support"));
                        break;
                }
            }
        }

        private ReplyModel GetJsonReply(WebhookModel value)
        {
            foreach (var daa in value.events)
            {
                try
                {

                    return LineHelper.ReturnSingleSting(daa.replyToken, JsonConvert.SerializeObject(value));

                }
                catch (Exception ex)
                {
                    return LineHelper.ReturnSingleSting(daa.replyToken, JsonConvert.SerializeObject(ex));

                }
            }




            return null;
        }

        private ReplyModel GetFollowReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }
        private ReplyModel GetLeaveReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }
        private ReplyModel GetUnfollowReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }
        private ReplyModel GetPostbackReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }
        private ReplyModel GetMemberLeftReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }
        private ReplyModel GetmemberJoinReply(Event msg)
        {
            return LineHelper.ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));

        }


        private ReplyModel HandleMessageObject(Event msg)
        {
            //   return GetTextReply(msg.replyToken, JsonConvert.SerializeObject(msg));
            if (msg.message != null)
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
            else if (msg.postback != null)
            {
                return GetPostbackReply(msg);

            }

            else
                return null;
        }
        private ReplyModel GetTextReply(Event msg)
        {
            return LineHelper.SamgoReply(msg);

        }
        private ReplyModel GetTextReply(string token, string text, Event msg=null)
        {

            return LineHelper.SamgoReply(token, text, msg);

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
    public class APIHelper
    {
        private readonly static string channelToken = "Wc4sjM/y5LsuQQwW6yX1y4UUjjYEtrmdx+i3YaivMXXaW0ZD9jbEC6rZoHKHizqvX2JNy+wykltXk8RQ/DQN9wkhwL9yIS7cemBAmoUbxl5pKFAYxQWNBq1MyqH3Df8wLJslkvQmeZWTZqjwprQ/dQdB04t89/1O/w1cDnyilFU=";

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
