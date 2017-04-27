using System.Collections.Generic;
using System.Threading.Tasks;

namespace paylive.sms
{
    public class SmsApi
    {
        public async Task<string> Send(string templateid, string mobile, string content)
        {
            var smsurl = Config.SmsApi;
            var appSecret = Config.AppSecret;
            var nonce = Core.GenerateNonceStr();
            var curTime = Core.GenerateTimeStamp();
            var checkSum = Core.MakeSign(appSecret, nonce, curTime);
            var body = "templateid={0}&mobiles=[\"{1}\"]&params=[\"{2}\"]";
            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("AppKey", Config.AppKey));
            headers.Add(new KeyValuePair<string, string>("CurTime", curTime));
            headers.Add(new KeyValuePair<string, string>("CheckSum", checkSum));
            headers.Add(new KeyValuePair<string, string>("Nonce", nonce));
            return await HttpService.Post(string.Format(body, templateid, mobile, content), smsurl, headers);
        }
    }
}