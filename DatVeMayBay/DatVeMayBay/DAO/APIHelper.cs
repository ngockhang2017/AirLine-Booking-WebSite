using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace DatVeMayBay.Dao
{
    public class APIHelper
    {
        private static readonly string url = "https://localhost:44380/";
        private static readonly string restfull = url + "api/";
        // GET
        public static T SendGetRequest<T>(string path)
        {
            T data = default(T);
            using (var client = new HttpClient())
            {
                var req = client.GetAsync(restfull + path);
                HttpResponseMessage res = req.Result;
                var result = res.Content.ReadAsStringAsync().Result;
                if (res.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<T>(result);
                }
            }
            return data;
        }


        // POST
        public static T SendPostRequest<T>(string path, T p)
        {
            T objectOut = default(T);
            using (var client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(p);
                var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(restfull);
                var res = client.PostAsync(path, httpContent).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    objectOut = JsonConvert.DeserializeObject<T>(result);

                }

            }
            return objectOut;
        }
        // PUT
        public static T SendPutRequest<T>(string path, T p)
        {
            T objectOut = default(T);
            using (var client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(p);
                client.BaseAddress = new Uri(restfull);
                var res = client.PutAsync(path + data, null).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    objectOut = JsonConvert.DeserializeObject<T>(result);
                    return objectOut;
                }
            }
            return objectOut;
        }
        // DELETE
        public static bool SendDeleteRequest(string path, int id)
        {
            bool objectOut = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(restfull);
                var res = client.DeleteAsync(path + "?id=" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    objectOut = JsonConvert.DeserializeObject<bool>(result);
                    return objectOut;
                }
            }
            return objectOut;
        }
    }
}
