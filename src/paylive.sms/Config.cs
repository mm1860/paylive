using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.sms
{
    public class Config
    {
        public static string SmsApi { get; set; }
        public static string AppKey { get; set; }
        public static string AppSecret { get; set; }

        public Config()
        {
            SmsApi = "https://api.netease.im/sms/sendtemplate.action";
            AppKey = "cc0f595b04eb42d18b78fb39f3baeb46";
            AppSecret = "ce025c4a01984759a14c1e0cf8a932a3";
        }
    }
}
