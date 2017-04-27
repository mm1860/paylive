using System;
using System.Security.Cryptography;
using System.Text;

namespace paylive.sms
{
    public class Core
    {
        /**
* @生成签名，详见签名生成算法
* @return 签名, sign字段不参加签名
*/

        public static string MakeSign(string appSecret, string nonce, string curTime)
        {
            var sha1 = SHA1.Create();
            var bs = sha1.ComputeHash(Encoding.UTF8.GetBytes(appSecret + nonce + curTime));
            var sb = new StringBuilder();
            foreach (var b in bs)
                sb.Append(b.ToString("x2"));
            //所有字符转为大写
            return sb.ToString().ToLower();
        }

        /**
       * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        * @return 时间戳
       */

        public static string GenerateTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */

        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}