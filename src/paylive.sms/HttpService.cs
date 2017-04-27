using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace paylive.sms
{
    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        public static async Task<string> Post(string body, string url,List<KeyValuePair<string,string>> headers)
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
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            foreach (var header in headers)
            {
                content.Headers.Add(header.Key, header.Value);
            }
            var ret = await client.PostAsync(url, content);
            result = await ret.Content.ReadAsStringAsync();
            return result;
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
