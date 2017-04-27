using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace paylive.core
{
    /// <summary>
    ///     http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        public static async Task<string> Post(string xml, string url, bool isUseCert, int timeout)
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

            var data = Encoding.UTF8.GetBytes(xml);
            var ret = await client.PostAsync(url, new ByteArrayContent(data));
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