using System;
using paylive.core;

namespace paylive.wxpay
{
    public class WxService : Plug
    {
        public WxService()
        {
            Url = "/wx/index";
            Ua = "micro";
            Descrpion = "wechat";
        }

        public override string Run()
        {
            throw new NotImplementedException();
        }
    }
}