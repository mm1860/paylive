using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.Console.Core
{
    public class Config
    {
        public static string _Sendsms = "https://webim.feixin.10086.cn/Views/WebIM/SendSMS.aspx?Version=54";
        public static string _GetSmsPwd = "https://webim.feixin.10086.cn/WebIM/GetSmsPwd.aspx";
        public static string _Login = "https://webim.feixin.10086.cn/WebIM/Login.aspx";
        public static string _GetConnect = "https://webim.feixin.10086.cn/WebIM/GetConnect.aspx?Version={0}";
    }
}
