using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace HerbMagicWebApi.Common
{
    public class HttpHelper
    {
        //public T MakeRequest<T>(string url)
        //{
        //    var client = new HttpClient();
        //    //var queryString = HttpUtility.ParseQueryString(string.Empty);

        //    //// This app ID is for a public sample app that recognizes requests to turn on and turn off lights
        //    //var luisAppId = "df67dcdb-c37d-46af-88e1-8b97951ca1c2";
        //    //var subscriptionKey = "YOUR_SUBSCRIPTION_KEY";

        //    //// The request header contains your subscription key
        //    //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        //    //// The "q" parameter contains the utterance to send to LUIS
        //    //queryString["q"] = "turn on the left light";

        //    //// These optional request parameters are set to their default values
        //    //queryString["timezoneOffset"] = "0";
        //    //queryString["verbose"] = "false";
        //    //queryString["spellCheck"] = "false";
        //    //queryString["staging"] = "false";

        //    //var uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
        //    var response = client.GetAsync(url).Result;
        //    var strResponseContent = response.Content.ReadAsStringAsync().Result;
        //    return JsonConvert.DeserializeObject<T>(strResponseContent);
        //}

        public static T GetRequest<T>(string url)
        {

            return MakeRequest<T>("GET", url);
        }

        public static T PostRequest<T>(string url, string data, string contenType = "application/json")
        {
            return MakeRequest<T>("POST", url, data, contenType);
        }

        private static T MakeRequest<T>(string method, string url, string data = null, string contenType = "application/json")
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = contenType;
                httpWebRequest.Method = method;
                if (data != null)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var strResoult = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(strResoult);
                }
            }
            catch (Exception ex) { return default(T); }
        }
    }
}