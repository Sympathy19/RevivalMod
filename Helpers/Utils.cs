using Newtonsoft.Json;
using Comfort.Common;
using SPT.Common.Http;
using System.Collections.Generic;
using EFT;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using SPT.Common.Utils;
using System.Text;

namespace RevivalMod.Helpers
{
    internal class Utils
    {
        public static HttpClient Client { get; set; } = new HttpClient();

        public enum RequestMethod
        {
            GET, POST, PUT
        }

        //public static async Task<string> GetURLAsync(string url)
        //{
        //    try
        //    {
        //        HttpResponseMessage Response = await Client.GetAsync(url);

        //        if (!Response.IsSuccessStatusCode) return string.Empty;
        //        else return await Response.Content.ReadAsStringAsync();
        //    }
        //    catch(Exception Ex) { Plugin.LogSource.LogInfo(Ex.ToString()); }

        //    return string.Empty;
        //}



        public static async Task<string> PostJsonAsync(string path, string json)
        {
            Plugin.LogSource.LogInfo("[REQUEST]: " + path);
            Plugin.LogSource.LogError($"postJson -> {json}");
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            byte[] bytes2 = await RequestHandler.HttpClient.PostAsync(path, bytes);
            if(bytes2 is null || bytes2.Length == 0)
            {
                Plugin.LogSource.LogError($"Server Response was empty...");
                return string.Empty;
            }

            string @string = Encoding.UTF8.GetString(bytes2);
            Plugin.LogSource.LogError($"postJson -> returned string; {@string}");

            //ValidateJson(path, @string);
            return @string;
        }

        /// <summary>
        /// debugging post json
        /// </summary>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostJson(string path, string json) => Task.Run(() => PostJsonAsync(path, json)).GetAwaiter().GetResult();

        //public static string SendEmptyJSON<T>(string url)
        //{
        //    string json = JsonConvert.SerializeObject();
        //    var req = RequestHandler.PostJson(url, "{}");
        //    Plugin.LogSource.LogInfo(req);
        //    //return JsonConvert.DeserializeObject<T>(req);
        //}

        public static T ServerRoute<T>(string url, object data = default(object))
        {
            string json = JsonConvert.SerializeObject(data);
            var req = RequestHandler.PostJson(url, json);
            return JsonConvert.DeserializeObject<T>(req);
        }
        public static string ServerRoute(string url, object data = default(object))
        {
            string json;
            if (data is string)
            {
                Dictionary<string, string> dataDict = new Dictionary<string, string>();
                dataDict.Add("data", (string)data);
                json = JsonConvert.SerializeObject(dataDict);
            }
            else
            {
                json = JsonConvert.SerializeObject(data);
            }

            return RequestHandler.PutJson(url, json);
        }

        public static Player GetYourPlayer() {
            Player player = Singleton<GameWorld>.Instance.MainPlayer;
            if (player == null) return null;          
            if (!player.IsYourPlayer) return null;
            return player;
        }

    }
}
