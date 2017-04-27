using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace paylive.Console.Core
{
    public class HttpServiceResult<T>
    {
        public ResultModel<T> StringBody { get; set; }
        public CookieCollection CookieCollection { get; set; }
    }

    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        public static async Task<HttpServiceResult<T>> Post<T>(string body, string url)
        {
            var result = "";
            var client = new HttpClient();
            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            //设置https验证方式
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                handler.ServerCertificateCustomValidationCallback = CheckValidationResult;
                client = new HttpClient(handler);
            }
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            
            var ret = await client.PostAsync(url, content);
            var getCookies = handler.CookieContainer.GetCookies(new Uri(url));
            
            result = await ret.Content.ReadAsStringAsync();
            return new HttpServiceResult<T>()
            {
                StringBody = JsonConvert.DeserializeObject<ResultModel<T>>(result),
                CookieCollection = getCookies
            };
        }

        /// <summary>
        ///     处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static async Task<string> Get(string url)
        {
            var result = "";
            var client = new HttpClient();

            //设置https验证方式
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = CheckValidationResult;
                client = new HttpClient(handler);
            }

            result = await client.GetStringAsync(url);
            return result;
        }
    }
}
